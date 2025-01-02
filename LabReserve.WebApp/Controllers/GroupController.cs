using FluentValidation;
using LabReserve.Application.Extensions;
using LabReserve.Application.UseCases.Courses.GetCourses;
using LabReserve.Application.UseCases.Groups.CreateGroup;
using LabReserve.Application.UseCases.Groups.DeleteGroup;
using LabReserve.Application.UseCases.Groups.GetGroup;
using LabReserve.Application.UseCases.Groups.GetGroups;
using LabReserve.Application.UseCases.Groups.UpdateGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers
{
    [Authorize("admin")]
    public class GroupController(IMediator mediator) : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("{controller}/list")]
        public async Task<IActionResult> ListAll([FromQuery] GetGroupsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(query, cancellationToken);
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest("error when listing groups");
            }
        }

        [HttpGet]
        [Route("{controller}/{groupId}")]
        public async Task<IActionResult> Get(long groupId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetGroupQuery(groupId), cancellationToken);
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest("error returning group");
            }
        }

        [HttpPut]
        [Route("{controller}/{groupId}")]
        public async Task<IActionResult> Update(long groupId, [FromBody] UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return BadRequest("request is null");

                request.GroupId = groupId;

                await mediator.Send(request, cancellationToken);
                return Ok("group updated");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.GetValidationErrors());
            }
            catch (Exception)
            {
                return BadRequest("error updating group");
            }
        }

        [HttpPost]
        [Route("{controller}")]
        public async Task<IActionResult> Create([FromBody] CreateGroupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return BadRequest("request is null");

                await mediator.Send(request, cancellationToken);
                return Ok("group created");
            }
            catch (ValidationException e)
            {
                return BadRequest(e.GetValidationErrors());
            }
            catch (Exception)
            {
                return BadRequest("error creating group");
            }
        }

        [HttpDelete]
        [Route("{controller}/{groupId}")]
        public async Task<IActionResult> Delete(long groupId, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(new DeleteGroupCommand(groupId), cancellationToken);
                return Ok("group deleted");
            }
            catch (Exception)
            {
                return BadRequest("error deleting group");
            }
        }

        [HttpGet]
        [Route("{controller}/courses")]
        public async Task<IActionResult> ListCourses([FromQuery] GetCoursesQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(query, cancellationToken);
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest("error when listing courses");
            }
        }
    }
}
