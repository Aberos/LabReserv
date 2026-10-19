using FluentValidation;
using LabReserve.Application.Extensions;
using LabReserve.Application.UseCases.Groups.GetGroups;
using LabReserve.Application.UseCases.Reservations.CreateReservation;
using LabReserve.Application.UseCases.Reservations.DeleteReservation;
using LabReserve.Application.UseCases.Reservations.GetReservation;
using LabReserve.Application.UseCases.Reservations.GetReservations;
using LabReserve.Application.UseCases.Reservations.UpdateReservation;
using LabReserve.Application.UseCases.Rooms.GetRooms;
using LabReserve.Application.UseCases.Users.GetUsers;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers;

[Authorize]
public class ReservationController(IMediator mediator) : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("{controller}/events")]
    public async Task<IActionResult> Events([FromQuery] GetReservationsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            return Json(await mediator.Send(query, cancellationToken));
        }
        catch (ValidationException e)
        {
            return BadRequest(e.GetValidationErrors());
        }
        catch (Exception)
        {
            return BadRequest("error when listing reservations");
        }
    }

    [HttpGet("{controller}/{reservationId:long}")]
    public async Task<IActionResult> Get(long reservationId, CancellationToken cancellationToken)
    {
        try
        {
            return Json(await mediator.Send(new GetReservationQuery(reservationId), cancellationToken));
        }
        catch (Exception)
        {
            return BadRequest("error returning reservation");
        }
    }

    [Authorize("scheduler")]
    [HttpPost("{controller}")]
    public async Task<IActionResult> Create([FromBody] CreateReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(request, cancellationToken);
            return Ok("reservation created");
        }
        catch (ValidationException e)
        {
            return BadRequest(e.GetValidationErrors());
        }
        catch (Exception)
        {
            return BadRequest("error creating reservation");
        }
    }

    [Authorize("scheduler")]
    [HttpPut("{controller}/{reservationId:long}")]
    public async Task<IActionResult> Update(long reservationId, [FromBody] UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            request.ReservationId = reservationId;
            await mediator.Send(request, cancellationToken);
            return Ok("reservation updated");
        }
        catch (ValidationException e)
        {
            return BadRequest(e.GetValidationErrors());
        }
        catch (Exception)
        {
            return BadRequest("error updating reservation");
        }
    }

    [Authorize("scheduler")]
    [HttpDelete("{controller}/{reservationId:long}")]
    public async Task<IActionResult> Delete(long reservationId, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new DeleteReservationCommand(reservationId), cancellationToken);
            return Ok("reservation deleted");
        }
        catch (Exception)
        {
            return BadRequest("error deleting reservation");
        }
    }

    [HttpGet("{controller}/rooms")]
    public async Task<IActionResult> Rooms([FromQuery] GetRoomsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            return Json(await mediator.Send(query, cancellationToken));
        }
        catch (Exception)
        {
            return BadRequest("error when listing rooms");
        }
    }

    [Authorize("scheduler")]
    [HttpGet("{controller}/groups")]
    public async Task<IActionResult> Groups([FromQuery] GetGroupsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            return Json(await mediator.Send(query, cancellationToken));
        }
        catch (Exception)
        {
            return BadRequest("error when listing groups");
        }
    }

    [Authorize("scheduler")]
    [HttpGet("{controller}/users")]
    public async Task<IActionResult> Users([FromQuery] GetUsersQuery query, CancellationToken cancellationToken)
    {
        try
        {
            return Json(await mediator.Send(query, cancellationToken));
        }
        catch (Exception)
        {
            return BadRequest("error when listing users");
        }
    }
}
