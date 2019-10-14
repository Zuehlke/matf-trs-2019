using System.Web.Mvc;
using eBidder.Models;
using eBidder.Services;

namespace eBidder.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController()
        {
            _userService = new UserService();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View(userViewModel);
            }

            var user = _userService.GetByUsername(userViewModel.Username);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User {userViewModel.Username} doesn't exist.";
                return View(userViewModel);
            }

            if (user.Password != userViewModel.Password)
            {
                ViewBag.ErrorMessage = "Wrong password.";
                return View(userViewModel);
            }

            UserSession.CurrentUser = userViewModel;
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            UserSession.CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }
    }
}