using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Filter
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (ctx.HttpContext.Session["user"] == null)
            {
                ctx.Result = new RedirectResult("/professor/login");
            }
            base.OnActionExecuting(ctx);
        }
    }
}