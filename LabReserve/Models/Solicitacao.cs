using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class Solicitacao
    {
        public int idSolicitacao { get; set; }
        public int idProfessor { get; set; }

        public string nomeProfessor { get; set; }
        public string sobrenomeProfessor { get; set; }
        public int idSala { get; set; }
        public string nomeSala { get; set; }

        public int idTurma { get; set; }
        public string nomeTurma { get; set; }

        public int idCurso { get; set; }
        public string nomeCurso { get; set; }

        public int Dia { get; set; }

        public int Turno { get; set; }

        public int Bloco { get; set; }

        public int Status { get; set; }
    }
}