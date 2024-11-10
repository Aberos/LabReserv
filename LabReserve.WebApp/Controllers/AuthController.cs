using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LabReserve.WebApp.Domain.Abstractions;
using LabReserve.WebApp.Domain.Dto;
using LabReserve.WebApp.Infrastructure;

namespace LabReserve.WebApp.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IUserService _service;

        public AuthController(IAuthUser authUser, IUserService service) : base(authUser)
        {
            _service = service;
        }

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
        public async Task<IActionResult> SignIn([FromBody] AuthRequestDto request)
        {
            try
            {
                var result = await _service.SignIn(request.Email, request.Password) ?? throw new Exception("User not found");
                await HttpContext.SetAuthUser(result);

                return Json(new AuthResponseDto(result.Email, result.UserType, result.Name));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{controller}/sign-out")]
        public async Task<IActionResult> SingOut()
        {
            await HttpContext.RemoveAuthUser();

            return Ok();
        }

        [HttpGet]
        [Route("{controller}/user")]
        public IActionResult UserAuth()
        {
            return Json(AuthUser);
        }
    }
}
