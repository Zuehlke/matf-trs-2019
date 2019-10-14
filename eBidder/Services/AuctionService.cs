using System;
using System.Collections.Generic;
using System.Linq;
using eBidder.Mappers;
using eBidder.Models;
using eBidder.Repositories;

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

        public AuctionViewModel CreateAuction(string username, AuctionItemViewModel auctionItemViewModel, int auctionState = 1)
        {
            var user = _userRepository.GetByUsername(username);
            var newAuction = CreateNewAuctionViewModel(username, auctionItemViewModel, auctionState);
            var auction = _auctionRepository.CreateAuction(newAuction.ToAuction(user));
            _auditRepository.CreateRecord(username, auction.AuctionId.ToString(), "Auction created");

            return auction.ToAuctionViewModel();
        }

        public bool PlaceBid(string username, AuctionViewModel auctionViewModel)
        {
            if (UserSameAsSeller(username, auctionViewModel))
            {
                throw new InvalidOperationException("Users are not allowed to bid for their own auctions.");
            }

            if (BidAmountIsLessThanMinAmount(auctionViewModel))
            {
                throw new ArgumentException("Bid must not be less than minimum amount for auction item.");
            }

            var user = _userRepository.GetByUsername(username);
            var seller = _userRepository.GetByUsername(auctionViewModel.Seller);

            return _auctionRepository.PlaceBid(user, auctionViewModel.ToAuction(seller), float.Parse(auctionViewModel.BidAmount));
        }

        public void CloseAuction(AuctionViewModel auctionViewModel)
        {
            var user = _userRepository.GetByUsername(auctionViewModel.Seller);
            _auctionRepository.CloseAuction(auctionViewModel.ToAuction(user));
        }

        public IEnumerable<AuctionViewModel> GetAllAuctions()
        {
            return _auctionRepository.GetAuctions().Select(a => a.ToAuctionViewModel());
        }

        public IEnumerable<AuctionViewModel> GetOpenAuctions()
        {
            return _auctionRepository.GetAuctions().Where(a => (int) a.AuctionState == 1).Select(a => a.ToAuctionViewModel());
        }

        public IEnumerable<AuctionViewModel> GetAuctionsByUsername(string username)
        {
            return _auctionRepository.GetAuctionByUsername(username).Select(a => a.ToAuctionViewModel());
        }

        public IEnumerable<AuctionViewModel> GetAuctionsWithUsersBid(string username)
        {
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
    }
}