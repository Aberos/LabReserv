using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers;

[Authorize("admin")]
public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}