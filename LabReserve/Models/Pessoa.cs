using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sobrenome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string Celular { get; set; }

        public int Status { get; set; }
    }
}