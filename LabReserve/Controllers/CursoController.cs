using LabReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Controllers
{
    public class CursoController : Controller
    {
        // GET: Curso
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (CursoModel model = new CursoModel())
            {
                Curso e = new Curso();
                e.Nome = form["Nome"];
                model.Create(e);
            }

            return RedirectToAction("ManagerCurso", "Admin");
        }


        public ActionResult Delete(int id)
        {
            using(CursoModel model = new CursoModel())
            {
                model.Delete(id);
            }

            return RedirectToAction("ManagerCurso", "Admin");
        }

    }
}