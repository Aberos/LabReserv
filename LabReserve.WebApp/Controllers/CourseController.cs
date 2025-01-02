using System.ComponentModel.DataAnnotations;
using LabReserve.Application.UseCases.Courses.CreateCourse;
using LabReserve.Application.UseCases.Courses.DeleteCourse;
using LabReserve.Application.UseCases.Courses.GetCourse;
using LabReserve.Application.UseCases.Courses.GetCourses;
using LabReserve.Application.UseCases.Courses.UpdateCourse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers
{
    [Authorize("admin")]
    public class CourseController(IMediator mediator) : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("{controller}/list")]
        public async Task<IActionResult> ListAll([FromQuery] GetCoursesQuery query, CancellationToken cancellationToken)
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

        [HttpGet]
        [Route("{controller}/{courseId}")]
        public async Task<IActionResult> Get(long courseId, CancellationToken cancellationToken)
        {
            try
            {
                var result = await mediator.Send(new GetCourseQuery(courseId), cancellationToken);
                return Json(result);
            }
            catch (Exception)
            {
                return BadRequest("error returning course");
            }
        }

        [HttpPut]
        [Route("{controller}/{courseId}")]
        public async Task<IActionResult> Update(long courseId, [FromBody] UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return BadRequest("request is null");

                request.CourseId = courseId;

                await mediator.Send(request, cancellationToken);
                return Ok("course updated");
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                return BadRequest("error updating course");
            }
        }

        [HttpPost]
        [Route("{controller}")]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request is null)
                    return BadRequest("request is null");

                await mediator.Send(request, cancellationToken);
                return Ok("course created");
            }
            catch (ValidationException e)
            {
                return BadRequest(e);
            }
            catch (Exception)
            {
                return BadRequest("error creating courses");
            }
        }

        [HttpDelete]
        [Route("{controller}/{courseId}")]
        public async Task<IActionResult> Delete(long courseId, CancellationToken cancellationToken)
        {
            try
            {
                await mediator.Send(new DeleteCourseCommand(courseId), cancellationToken);
                return Ok("course deleted");
            }
            catch (Exception)
            {
                return BadRequest("error deleting course");
            }
        }
    }
}
