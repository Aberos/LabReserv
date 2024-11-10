using LabReserve.WebApp.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        public IAuthUser AuthUser { get; private set; }
        protected BaseController(IAuthUser authUser)
        {
            AuthUser = authUser;
        }
    }
}
