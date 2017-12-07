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

            using(SalaModel model = new SalaModel())
            {
                ViewBag.Salas = model.Read();
            }

            using (TurmaModel model = new TurmaModel())
            {
                ViewBag.Turmas = model.Read();
            }

            using (ReservaModel model =  new ReservaModel())
            {
                ViewBag.Reservas = model.Read();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            using (ReservaModel model = new ReservaModel())
            {
                Reserva e = new Reserva();
                e.Data = form["dia"];
                e.Turno = Convert.ToInt32(form["turno"]);
                e.Bloco = Convert.ToInt32(form["bloco"]);
                e.Sala.Id = Convert.ToInt32(form["sala"]);
                e.Professor.Id = Convert.ToInt32(form["professor"]);
                e.Turma.Id = Convert.ToInt32(form["turma"]);
                e.Admin.Id = (Session["user"] as Admin).Id;

                if(model.CheckReserva(e.Sala.Id, e.Data, e.Turno, e.Bloco) == false)
                {
                    TempData["reserva"] = "Reserva Criada!";
                    model.Create(e);
                }else
                {
                    TempData["reserva"] = "Ja existe uma reserva para esta sala neste dia ,turno e bloco";
                }
                
            }
            
            return RedirectToAction("Reservas");
        }

        public ActionResult Delete(int id)
        {
            using (ReservaModel model = new ReservaModel())
            {
                model.Delete(id);
            }

            return RedirectToAction("Reservas");
        }


        [SessionFilter]
        public ActionResult Professor()
        {
            try
            {
                using (ReservaModel model = new ReservaModel())
                {
                    int id = (Session["user"] as Professor).Id;
                    ViewBag.Reservas = model.ReadByProfessor(id);
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Login", "Professor");
            }          
        }
    }
}