using LabReserve.Application.UseCases.Users.UserSignIn;
using LabReserve.Application.UseCases.Users.UserSignOut;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers
{
    public class AuthController(IMediator mediator) : Controller()
    {

        public IActionResult Index()
        {
            return View();
        }

        [Route("access-denied")]
        public IActionResult AccessDenied() 
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("{controller}/sing-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(request, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{controller}/sign-out")]
        public async Task<IActionResult> SingOut(CancellationToken cancellationToken)
        {
            await mediator.Send(new SignOutUserCommand(), cancellationToken);
            return Redirect("/Auth");
        }
    }
}
