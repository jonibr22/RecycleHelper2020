using RecycleHelperApplication.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RecycleHelperApplication.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            //Check to see if we need to skip authentication
            if (ctx.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                    || ctx.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
                return;

            //If it's not even authenticated redirect to a login action
            //  ...remember, you need [AllowAnonymous] on Login action to prevent an endless redirect loop
            if (Session[SessionEnum.USER_ID] == null)
            {
                ctx.Result = new RedirectToRouteResult(
                                 new RouteValueDictionary(new { controller = "Auth", action = "Index" })
                             );
                return;
            }
        }
    }
}