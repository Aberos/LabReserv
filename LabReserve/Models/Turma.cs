using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class Turma
    {
        private Curso curso;

        public Turma()
        {
            curso = new Curso();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public Curso Curso { get { return curso; } set { this.curso = value; } }
    }
}