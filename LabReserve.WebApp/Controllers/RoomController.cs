using FluentValidation;
using LabReserve.Application.UseCases.Rooms.CreateRoom;
using LabReserve.Application.UseCases.Rooms.DeleteRoom;
using LabReserve.Application.UseCases.Rooms.GetRoom;
using LabReserve.Application.UseCases.Rooms.GetRooms;
using LabReserve.Application.UseCases.Rooms.UpdateRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers
{
    [Authorize("admin")]
    public class RoomController(IMediator mediator) : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("{controller}/list")]
        public async Task<IActionResult> ListAll([FromQuery] GetRoomsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(query, cancellationToken);
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest("error when listing rooms");
            }
        }

        [HttpGet]
        [Route("{controller}/{roomId}")]
        public async Task<IActionResult> Get(long roomId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetRoomQuery(roomId), cancellationToken);
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest("error returning room");
            }
        }

        [HttpPut]
        [Route("{controller}/{roomId}")]
        public async Task<IActionResult> Update(long roomId, [FromBody] UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return BadRequest("request is null");

                request.RoomId = roomId;

                await mediator.Send(request, cancellationToken);
                return Ok("room updated");
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                return BadRequest("error updating room");
            }
        }

        [HttpPost]
        [Route("{controller}")]
        public async Task<IActionResult> Create([FromBody] CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return BadRequest("request is null");

                await mediator.Send(request, cancellationToken);
                return Ok("room created");
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                return BadRequest("error creating room");
            }
        }

        [HttpDelete]
        [Route("{controller}/{roomId}")]
        public async Task<IActionResult> Delete(long roomId, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(new DeleteRoomCommand(roomId), cancellationToken);
                return Ok("room deleted");
            }
            catch (Exception)
            {
                return BadRequest("error deleting room");
            }
        }
    }
}
