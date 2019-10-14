using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eBidder.Anotations
{
    public class AuthorizeWithRedirect : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return UserSession.CurrentUser != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (UserSession.CurrentUser == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Login", action = "Login" }));
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}