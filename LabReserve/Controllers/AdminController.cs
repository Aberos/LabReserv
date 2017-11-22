using LabReserve.Models;
using LabReserve.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LabReserve.Controllers
{
    public class AdminController : Controller
    {
        [SessionFilter]
        // GET: Admin
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

            using (AdminModel model = new AdminModel())
            {
                Admin admin = model.Read(email, senha);

                if (admin == null)
                {
                    // nao autenticado
                    return RedirectToAction("Login");
                }
                else
                {

                    //Criar uma sessão (mantida durante toda a aplicação)
                    Session["user"] = admin;
                    //usuario valido
                    return RedirectToAction("Index", "Admin");
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


            using (AdminModel model = new AdminModel())
            {
                try
                {
                    Admin e = new Admin();
                    e.Nome = nome;
                    e.Sobrenome = sobrenome;
                    e.Celular = cel;
                    e.Email = email;
                    e.Senha = senha;
                    e.Status = 1;
                    model.Create(e);
                    return RedirectToAction("ManagerAdmin", "Admin");
                }
                catch
                {
                    return View();
                }

            }
        }


        [SessionFilter]
        public ActionResult ManagerProfessor()
        {
            using (ProfessorModel model = new ProfessorModel())
            {
                ViewBag.ListaProfessores = model.Read();
            }
            return View();
        }

        [SessionFilter]
        public ActionResult ManagerAdmin()
        {
            using (AdminModel model = new AdminModel())
            {
                ViewBag.ListaAdmin = model.Read();
            }
            return View();
        }

        [SessionFilter]
        public ActionResult ManagerSala()
        {
            return View();
        }

        [SessionFilter]
        public ActionResult ManagerTurma()
        {
            return View();
        }

        [SessionFilter]
        public ActionResult ManagerCurso()
        {
            return View();
        }

    }
}