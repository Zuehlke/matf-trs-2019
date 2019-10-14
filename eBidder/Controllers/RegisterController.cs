using System.Web.Mvc;
using eBidder.Models;
using eBidder.Services;

namespace eBidder.Controllers
{
    [AllowAnonymous]
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController()
        {
            _userService = new UserService();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View(userViewModel);
            }

            if (userViewModel.Password != userViewModel.ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Passwords don't match.";
                return View(userViewModel);
            }

            var userExists = _userService.GetByUsername(userViewModel.Username) != null;

            if (userExists)
            {
                ViewBag.ErrorMessage = $"User {userViewModel.Username} already exists.";
                return View(userViewModel);
            }

            UserSession.CurrentUser = _userService.CreateUser(userViewModel.Username, userViewModel.Password);
            return RedirectToAction("Index", "Home");
        }
    }
}