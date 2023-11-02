using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using vphone.Helper;

namespace vphone.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.Get<String>("Email") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Auth",
                        action = "Index"
                    })
                );
            }
        }
    }
}
