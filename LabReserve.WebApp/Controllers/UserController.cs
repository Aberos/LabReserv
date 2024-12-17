using LabReserve.Application.UseCases.Users.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers;

[Authorize("admin")]
public class UserController(IMediator mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("{controller}/list")]
    public async Task<IActionResult> ListAll([FromQuery]GetUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(query, cancellationToken);
            return Json(result);
        }
        catch (Exception ex) 
        { 
            return BadRequest("error when listing users");
        }
    }
}