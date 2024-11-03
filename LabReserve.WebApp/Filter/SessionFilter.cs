using Microsoft.AspNetCore.Mvc.Filters;

namespace LabReserve.WebApp.Filter
{
    public class SessionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            // if (ctx.HttpContext.Session["user"] == null)
            // {
            //     ctx.Result = new RedirectResult("/professor/login");
            // }
            base.OnActionExecuting(ctx);
        }
    }
}