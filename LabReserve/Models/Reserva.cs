using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public string Data { get; set; }

        public int Turno { get; set; }

        public int Bloco { get; set; }

        public Sala Sala { get; set; }

        public Admin Admin { get; set; }

        public Professor Professor { get; set; }

        public Turma Turma { get; set; }

        public int Status { get; set; }
    }
}