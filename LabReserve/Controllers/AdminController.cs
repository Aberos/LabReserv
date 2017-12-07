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
            using(SolicitacaoModel model = new SolicitacaoModel())
            {
                ViewBag.Solictacoes = model.Read();
            }

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
                    TempData["login"] = "Usuario invalido";
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
            using (SalaModel model = new SalaModel())
            {
                ViewBag.ListaSala = model.Read();
            }
            return View();
        }

        [SessionFilter]
        public ActionResult ManagerTurma()
        {
            using (CursoModel model = new CursoModel())
            {
                ViewBag.ListaCurso = model.Read();
            }

            using (TurmaModel model = new TurmaModel())
            {
                ViewBag.ListaTurma = model.Read();
            }
            return View();
        }

        [SessionFilter]
        public ActionResult ManagerCurso()
        {
            using(CursoModel model = new CursoModel())
            {
                ViewBag.ListaCurso = model.Read();
            }
            return View();
        }

        [SessionFilter]
        public ActionResult AddTurma(int id)
        {
            Professor e = null;
            using (ProfessorModel model = new ProfessorModel())
            {
                e = model.Read(id);
            }

            using(TurmaProfessorModel model = new TurmaProfessorModel())
            {
                ViewBag.Turmas = model.Read(id);
            }

            using (TurmaModel model = new TurmaModel())
            {
                ViewBag.ListaTurma = model.Read();
            }
            return View(e);
        }

        [SessionFilter]
        [HttpPost]
        public ActionResult AddTurma(FormCollection form)
        {
            int idProfessor = Convert.ToInt32(form["idProfessor"]);
            int idTurma =  Convert.ToInt32(form["turma"]);

            using(TurmaProfessorModel model = new TurmaProfessorModel())
            {
                model.Add(idProfessor, idTurma);
            }

            return RedirectToAction("AddTurma", "Admin", new { id = idProfessor });
        }

        [SessionFilter]
        public ActionResult DeleteProf(int id)
        {
            using(ProfessorModel model =  new ProfessorModel())
            {
                model.Delete(id);
            }

            return RedirectToAction("ManagerProfessor");
        }

        [SessionFilter]
        public ActionResult DeleteAdmin(int id)
        {
            using (AdminModel model = new AdminModel())
            {
                model.Delete(id);
            }
            return RedirectToAction("ManagerAdmin");
        }

        public ActionResult DeleteTurmaProf(int id, int idTurma)
        {
            using (TurmaProfessorModel model = new TurmaProfessorModel())
            {
                model.Remove(id, idTurma);
            }

            return RedirectToAction("AddTurma", "Admin", new { id = id });
        }

        public ActionResult RecusarSol(int id)
        {
            using (SolicitacaoModel model = new SolicitacaoModel())
            {
                Solicitacao e = new Solicitacao();
                e.idSolicitacao = id;
                e.Status = 3;

                model.UpdateStatus(e);
            }

            return RedirectToAction("Index");
        }


        public ActionResult AceitarSol(int id)
        {
            using (SolicitacaoModel model = new SolicitacaoModel())
            {
                Solicitacao e = new Solicitacao();
                e.idSolicitacao = id;
                e.Status = 2;

                model.UpdateStatus(e);
            }

            return RedirectToAction("Index");
        }



    }
}