using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Filter
{
    public class AdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if(ctx.HttpContext.Session["admin"] == null)
            {
                ctx.Result = new RedirectResult("/admin/login");
            }
            base.OnActionExecuting(ctx);
        }
    }
}