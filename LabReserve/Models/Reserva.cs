using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class Reserva
    {
        private Sala sala;
        private Admin admin;
        private Professor professor;
        private Turma turma;
        public Reserva()
        {
            sala = new Sala();
            admin = new Admin();
            professor = new Professor();
            turma = new Turma();
        }

        public int Id { get; set; }

        public string Data { get; set; }

        public int Turno { get; set; }

        public int Bloco { get; set; }

        public Sala Sala { get {return sala;} set {this.sala = value ;} }

        public Admin Admin { get { return admin; } set { this.admin = value; } }

        public Professor Professor { get { return professor; } set { this.professor = value; } }

        public Turma Turma { get { return turma; } set { this.turma = value; } }

        public int Status { get; set; }
    }
}