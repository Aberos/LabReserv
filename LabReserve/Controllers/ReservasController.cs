using LabReserve.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Controllers
{
    public class ReservasController : Controller
    {
        // GET: Reservas
        [SessionFilter]
        public ActionResult Reservas()
        {
            return View();
        }
    }
}