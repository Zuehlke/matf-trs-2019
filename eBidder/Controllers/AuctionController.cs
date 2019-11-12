using System;
using System.Linq;
using System.Web.Mvc;
using eBidder.Anotations;
using eBidder.Models;
using eBidder.Repositories;
using eBidder.Services;

namespace eBidder.Controllers
{
    [AuthorizeWithRedirect]
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;

        public AuctionController()
        {
            var unitOfWork = new UnitOfWork();
            _auctionService = new AuctionService();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Auctions";

            var auctionList = new AuctionListViewModel(_auctionService.GetOpenAuctions().ToList());
            return View(auctionList);
        }

        public ActionResult MyAuctions()
        {
            ViewBag.Title = "My auctions";

            var auctionList = new AuctionListViewModel(_auctionService.GetAuctionsByUsername(UserSession.CurrentUser.Username).ToList());
            return View("Index", auctionList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AuctionItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "All fields are required.";
                return View(viewModel);
            }

            float minAmount;
            if (!float.TryParse(viewModel.MinAmount, out minAmount))
            {
                ViewBag.ErrorMessage = "Wrong format for minimum amount. Please input the correct number.";
                return View();
            }

            _auctionService.CreateAuction(UserSession.CurrentUser.Username, viewModel);
            return RedirectToAction("Index");
        }

        public ActionResult Close(AuctionViewModel viewModel)
        {
            _auctionService.CloseAuction(viewModel);
            return RedirectToAction("Index");
        }

        public ActionResult MyBids()
        {
            ViewBag.Title = "My bids";

            var auctionList = new AuctionListViewModel(_auctionService.GetAuctionsWithUsersBid(UserSession.CurrentUser.Username).ToList());
            return View("Index", auctionList);
        }

        [HttpPost]
        public ActionResult PlaceBid(AuctionListViewModel allAuctions)
        {
            var auctionWithBid = allAuctions.ViewModels.FirstOrDefault(a => a.BidAmount != null);

            if (auctionWithBid == null)
            {
                throw new Exception("PlaceBid was called an no bid was detected for any auction.");
            }

            float amount;
            if (!float.TryParse(auctionWithBid.BidAmount, out amount))
            {
                ViewBag.ErrorMessage = "Wrong format for amount. Please input the correct number.";
                return View("Index");
            }

            try
            {
                _auctionService.PlaceBid(UserSession.CurrentUser.Username, auctionWithBid);
            }
            catch (Exception)
            {

                ViewBag.ErrorMessage = "Bit wasn't placed. Something went wrong.";
            }

            ClearAllBids(allAuctions);

            return RedirectToAction("Index");
        }

        private static void ClearAllBids(AuctionListViewModel allAuctions)
        {
            foreach (var auction in allAuctions.ViewModels)
            {
                auction.BidAmount = null;
            }
        }
    }
}