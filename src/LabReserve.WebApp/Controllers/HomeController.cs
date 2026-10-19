using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LabReserve.WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Reservation");
    }

    public IActionResult Error()
    {
        return View();
    }
}
