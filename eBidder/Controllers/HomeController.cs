using System.Web.Mvc;
using eBidder.Anotations;

namespace eBidder.Controllers
{
    [AuthorizeWithRedirect]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}