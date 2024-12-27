using System.ComponentModel.DataAnnotations;
using LabReserve.Application.UseCases.Users.CreateUser;
using LabReserve.Application.UseCases.Users.GetUser;
using LabReserve.Application.UseCases.Users.GetUsers;
using LabReserve.Application.UseCases.Users.UpdateUser;
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
    public async Task<IActionResult> ListAll([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(query, cancellationToken);
            return Json(result);
        }
        catch (Exception)
        {
            return BadRequest("error when listing users");
        }
    }

    [HttpGet]
    [Route("{controller}/{userId}")]
    public async Task<IActionResult> Get(long userId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new GetUserQuery(userId), cancellationToken);
            return Json(result);
        }
        catch (Exception)
        {
            return BadRequest("error returning user");
        }
    }

    [HttpPut]
    [Route("{controller}/{userId}")]
    public async Task<IActionResult> Update(long userId, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request is null)
                return BadRequest("request is null");

            request.UserId = userId;

            await mediator.Send(request, cancellationToken);
            return Ok("user updated");
        }
        catch (ValidationException e)
        {
            return BadRequest(e);
        }
        catch (Exception)
        {
            return BadRequest("error updating user");
        }
    }

    [HttpPost]
    [Route("{controller}")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request is null)
                return BadRequest("request is null");

            await mediator.Send(request, cancellationToken);
            return Ok("user created");
        }
        catch (ValidationException e)
        {
            return BadRequest(e);
        }
        catch (Exception)
        {
            return BadRequest("error creating user");
        }
    }
}