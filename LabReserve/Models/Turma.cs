using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class Turma
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public Curso Curso { get; set; }
    }
}