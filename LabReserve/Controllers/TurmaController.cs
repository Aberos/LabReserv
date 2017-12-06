using LabReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Controllers
{
    public class TurmaController : Controller
    {
        // GET: Turma
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using(TurmaModel model = new TurmaModel())
            {
                Turma e = new Turma();
                e.Nome = form["Nome"];
                e.Curso.Id = Convert.ToInt32(form["Curso"]);
                model.Create(e);
            }

            return RedirectToAction("ManagerTurma", "Admin");
        }


        public ActionResult Delete(int id)
        {
            using (TurmaModel model = new TurmaModel())
            {
                model.Delete(id);
            }
            return RedirectToAction("ManagerTurma", "Admin");
        }
    }
}