using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers
{
    public class AuthController(IUserService service) : Controller()
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
        public async Task<IActionResult> SignIn([FromBody] AuthRequestDto request)
        {
            try
            {
                var result = await service.SignIn(request.Email, request.Password) ?? throw new Exception("User not found");
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
            await service.SignOut();
            return Redirect("/Auth");
        }
    }
}
