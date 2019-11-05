using System;
using System.Collections.Generic;
using System.Linq;
using eBidder.Mappers;
using eBidder.Models;
using eBidder.Repositories;
using eBidder.Domain;

namespace eBidder.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        private readonly IUserRepository _userRepository;

        private readonly IAuditRepository _auditRepository;

        public AuctionService()
        {
            var unitOfWork = new UnitOfWork();
            _auctionRepository = unitOfWork.AuctionRepository;
            _userRepository = unitOfWork.UserRepository;
            _auditRepository = unitOfWork.AuditRepository;
        }

        public AuctionService(IAuctionRepository auctionRepository, IUserRepository userRepository, IAuditRepository auditRepository)
        {
            _auctionRepository = auctionRepository;
            _userRepository = userRepository;
            _auditRepository = auditRepository;
        }

        public AuctionViewModel CreateAuction(string username, AuctionItemViewModel auctionItemViewModel, int auctionState = 1)
        {
            if (auctionItemViewModel == null)
            {
                throw new ArgumentNullException($"AuctionItemViewModel must be provided");
            }

            var user = GetUser(username);
            if (user == null)
            {
                throw new ArgumentException($"User {username} doesn't exist");
            }

            var newAuction = CreateNewAuctionViewModel(username, auctionItemViewModel, auctionState);
            var auction = _auctionRepository.CreateAuction(newAuction.ToAuction(user));
            _auditRepository.CreateRecord(username, auction.AuctionId.ToString(), "Auction created");

            return auction.ToAuctionViewModel();
        }

        public AuctionViewModel CreateAuction(AuctionViewModel auctionViewModel)
        {
            if (auctionViewModel == null)
            {
                throw new ArgumentNullException($"AuctionItemViewModel must be provided");
            }

            var user = GetUser(auctionViewModel.Seller);
            if (user == null)
            {
                throw new ArgumentException($"User {auctionViewModel.Seller} doesn't exist");
            }

            var auction = _auctionRepository.CreateAuction(auctionViewModel.ToAuction(user));
            _auditRepository.CreateRecord(auctionViewModel.Seller, auction.AuctionId.ToString(), "Auction created");

            return auction.ToAuctionViewModel();
        }

        public AuctionViewModel PlaceBid(string username, AuctionViewModel auctionViewModel)
        {
            if (UserSameAsSeller(username, auctionViewModel))
            {
                throw new InvalidOperationException("Users are not allowed to bid for their own auctions");
            }

            if (BidAmountIsLessThanMinAmount(auctionViewModel))
            {
                throw new ArgumentException("Bid must not be less than minimum amount for auction item");
            }

            var user = GetUser(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} doesn't exist");
            }

            var seller = GetUser(auctionViewModel.Seller);
            if (seller == null)
            {
                throw new ArgumentException($"User {auctionViewModel.Seller} doesn't exist");
            }

            return _auctionRepository.PlaceBid(user, auctionViewModel.ToAuction(seller), float.Parse(auctionViewModel.BidAmount)).ToAuctionViewModel();
        }

        public AuctionViewModel CloseAuction(AuctionViewModel auctionViewModel)
        {
            var user = GetUser(auctionViewModel.Seller);
            if (user == null)
            {
                throw new ArgumentException($"User {auctionViewModel.Seller} doesn't exist");
            }

            return _auctionRepository.CloseAuction(auctionViewModel.ToAuction(user)).ToAuctionViewModel();
        }

        public IEnumerable<AuctionViewModel> GetAllAuctions()
        {
            return _auctionRepository.GetAuctions().Select(a => a.ToAuctionViewModel());
        }

        public IEnumerable<AuctionViewModel> GetOpenAuctions()
        {
            return _auctionRepository.GetAuctions().Where(a => (int)a.AuctionState == 1).Select(a => a.ToAuctionViewModel());
        }

        public IEnumerable<AuctionViewModel> GetAuctionsByUsername(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username must be provided");
            }

            return _auctionRepository.GetAuctionByUsername(username).Select(a => a.ToAuctionViewModel());
        }

        public IEnumerable<AuctionViewModel> GetAuctionsWithUsersBid(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException("Username must be provided");
            }

            return _auctionRepository.GetAuctionsWithUsersBid(username).Select(a => a.ToAuctionViewModel());
        }

        private static bool UserSameAsSeller(string username, AuctionViewModel auctionViewModel)
        {
            return username.Equals(auctionViewModel.Seller, StringComparison.OrdinalIgnoreCase);
        }

        private static bool BidAmountIsLessThanMinAmount(AuctionViewModel auctionViewModel)
        {
            return float.Parse(auctionViewModel.BidAmount) < float.Parse(auctionViewModel.MinAmount);
        }

        private static AuctionViewModel CreateNewAuctionViewModel(string username, AuctionItemViewModel auctionItemViewModel, int auctionState)
        {
            var newAuction = new AuctionViewModel
            {
                AuctionItem = auctionItemViewModel,
                MinAmount = auctionItemViewModel.MinAmount,
                Seller = username,
                AuctionState = auctionState,
                StartDate = DateTime.Today.AddMinutes(1),
                EndDate = DateTime.Today.AddDays(7)
            };
            return newAuction;
        }

        private User GetUser(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}