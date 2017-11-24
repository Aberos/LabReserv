using LabReserve.Filter;
using LabReserve.Models;
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

            using(ProfessorModel model = new ProfessorModel())
            {
                ViewBag.Professores = model.Read();
            }

            using(SalaModel modelSala = new SalaModel())
            {
                ViewBag.Salas = modelSala.Read();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            
            return RedirectToAction("Reservas");
        }
    }
}