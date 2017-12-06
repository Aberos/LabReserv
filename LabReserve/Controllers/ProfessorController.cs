using LabReserve.Filter;
using LabReserve.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabReserve.Controllers
{
    public class ProfessorController : Controller
    {
        // GET: Professor
        [SessionFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string email = form["Email"];
            string senha = form["Senha"];

            using (ProfessorModel model = new ProfessorModel())
            {
                Professor professor = model.Read(email, senha);

                if (professor == null)
                {
                    // nao autenticado
                    return RedirectToAction("Login");
                }
                else
                {

                    //Criar uma sessão (mantida durante toda a aplicação)
                    Session["user"] = professor;
                    //usuario valido
                    return RedirectToAction("Professor", "Reservas");
                }
            }
        }


        [SessionFilter]
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            string nome = form["Nome"];
            string sobrenome = form["Sobrenome"];
            string cel = form["Cel"];
            string email = form["Email"];
            string senha = form["Senha"];


            using (ProfessorModel model = new ProfessorModel())
            {
                try
                {
                    Professor e = new Professor();
                    e.Nome = nome;
                    e.Sobrenome = sobrenome;
                    e.Celular = cel;
                    e.Email = email;
                    e.Senha = senha;
                    e.Status = 1;
                    model.Create(e);
                    return RedirectToAction("ManagerProfessor", "Admin");
                }
                catch
                {
                    return View();
                }

            }
        }

        [SessionFilter]
        public ActionResult Turmas()
        {
            return View();
        }

        [SessionFilter]
        public ActionResult Solicitar()
        {
            return View();
        }
    }
}