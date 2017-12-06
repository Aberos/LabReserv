using LabReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Controllers
{
    public class SalaController : Controller
    {
        // GET: Sala
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (SalaModel model = new SalaModel())
            {
                Sala e = new Sala();
                e.Nome = form["Nome"];
                model.Create(e);
            }
            return RedirectToAction("ManagerSala", "Admin");
        }

        public ActionResult Delete(int id)
        {
            using (SalaModel model = new SalaModel())
            {
                model.Delete(id);
            }
            return RedirectToAction("ManagerSala", "Admin");
        }
    }
}