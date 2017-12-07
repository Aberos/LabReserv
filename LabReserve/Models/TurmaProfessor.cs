using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class TurmaProfessor 
    {
        public int idProfessor { get; set; }

        public int idTurma { get; set; }

        public string nomeTurma { get; set; }

        public int idCurso { get; set; }

        public string nomeCurso { get; set; }

        public int Status { get; set; }

    }
}