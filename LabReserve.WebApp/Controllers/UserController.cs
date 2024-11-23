using LabReserve.Domain.Abstractions;
using LabReserve.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers;

[Authorize("admin")]
public class UserController(IUserService service) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("{controller}/list")]
    public async Task<IActionResult> ListAll([FromQuery]FilterRequestDto filter)
    {
        try
        {
            var result = await service.GetAll(filter);
            return Json(result);
        }
        catch (Exception ex) 
        { 
            return BadRequest("error when listing users");
        }
    }
}