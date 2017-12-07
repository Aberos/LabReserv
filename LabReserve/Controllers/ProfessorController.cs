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
                    TempData["login"] = "Usuario invalido";
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
            using(TurmaProfessorModel model =  new TurmaProfessorModel())
            {
                int id = (Session["user"] as Professor).Id;
                ViewBag.Turmas = model.Read(id);
            }

            return View();
        }

        [SessionFilter]
        public ActionResult Solicitar()
        {

            using (SolicitacaoModel model =  new SolicitacaoModel())
            {
                int id = (Session["user"] as Professor).Id;
                ViewBag.Solictacoes = model.Read(id);
            }

            using (SalaModel model = new SalaModel())
            {
                ViewBag.Salas = model.Read();
            }

            using (TurmaProfessorModel model = new TurmaProfessorModel())
            {
                int id = (Session["user"] as Professor).Id;
                ViewBag.Turmas = model.Read(id);
            }

            return View();
        }

        [SessionFilter]
        [HttpPost]
        public ActionResult Solicitar(FormCollection form)
        {
            using (SolicitacaoModel model = new SolicitacaoModel())
            {
                Solicitacao e = new Solicitacao();
                e.idProfessor = (Session["user"] as Professor).Id;
                e.idSala = Convert.ToInt32(form["sala"]);
                e.idTurma = Convert.ToInt32(form["turma"]);
                e.Dia = Convert.ToInt32(form["dia"]);
                e.Turno = Convert.ToInt32(form["turno"]);
                e.Bloco = Convert.ToInt32(form["bloco"]);
                model.Create(e);
            }

            return RedirectToAction("Solicitar");
        }

        public ActionResult CancelarSolicitacao(int id)
        {
            using (SolicitacaoModel model = new SolicitacaoModel())
            {
                Solicitacao e = new Solicitacao();
                e.idSolicitacao = id;
                e.Status = 4;

                model.UpdateStatus(e);
            }

            return RedirectToAction("Solicitar");
        }
    }
}