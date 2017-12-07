using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class ReservaModel : ModelBase
    {
        public List<Reserva> Read()
        {
            List<Reserva> lista = new List<Reserva>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_reservas where Estatus = 1";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Reserva e = new Reserva();

                e.Id = (int)reader["idReserva"];
                e.Data = (string)reader["Dia"];
                e.Turno = (int)reader["Turno"];
                e.Bloco = (int)reader["Bloco"];
                e.Status = (int)reader["Estatus"];
                e.Sala.Id = (int)reader["idSala"];
                e.Sala.Nome = (string)reader["nomeSala"];
                e.Admin.Id = (int)reader["idAdmin"];
                e.Admin.Nome = (string)reader["nomeAdmin"];
                e.Professor.Id = (int)reader["idProfessor"];
                e.Professor.Nome = (string)reader["nomeProfessor"];
                e.Turma.Id = (int)reader["idTurma"];
                e.Turma.Nome = (string)reader["nomeTurma"];
                e.Turma.Curso.Id = (int)reader["idCurso"];
                e.Turma.Curso.Nome = (string)reader["nomeCurso"];


                lista.Add(e);
            }

            return lista;
        }

        public Reserva Read(int id)
        {
            Reserva e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_reservas where idReserva = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                e = new Reserva();
                e.Id = (int)reader["idReserva"];
                e.Data = (string)reader["Dia"];
                e.Turno = (int)reader["Turno"];
                e.Bloco = (int)reader["Bloco"];
                e.Status = (int)reader["Estatus"];
                e.Sala.Id = (int)reader["idSala"];
                e.Sala.Nome = (string)reader["nomeSala"];
                e.Admin.Id = (int)reader["idAdmin"];
                e.Admin.Nome = (string)reader["nomeAdmin"];
                e.Professor.Id = (int)reader["idProfessor"];
                e.Professor.Nome = (string)reader["nomeProfessor"];
                e.Turma.Id = (int)reader["idTurma"];
                e.Turma.Nome = (string)reader["nomeTurma"];
                e.Turma.Curso.Id = (int)reader["idCurso"];
                e.Turma.Curso.Nome = (string)reader["nomeCurso"];
            }

            return e;
        }

        public List<Reserva> ReadByProfessor(int id_Professor)
        {
            List<Reserva> lista = new List<Reserva>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_reservas where Estatus = 1 and idProfessor = @id";

            cmd.Parameters.AddWithValue("@id", id_Professor);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Reserva e = new Reserva();

                e.Id = (int)reader["idReserva"];
                e.Data = (string)reader["Dia"];
                e.Turno = (int)reader["Turno"];
                e.Bloco = (int)reader["Bloco"];
                e.Status = (int)reader["Estatus"];
                e.Sala.Id = (int)reader["idSala"];
                e.Sala.Nome = (string)reader["nomeSala"];
                e.Admin.Id = (int)reader["idAdmin"];
                e.Admin.Nome = (string)reader["nomeAdmin"];
                e.Professor.Id = (int)reader["idProfessor"];
                e.Professor.Nome = (string)reader["nomeProfessor"];
                e.Turma.Id = (int)reader["idTurma"];
                e.Turma.Nome = (string)reader["nomeTurma"];
                e.Turma.Curso.Id = (int)reader["idCurso"];
                e.Turma.Curso.Nome = (string)reader["nomeCurso"];


                lista.Add(e);
            }

            return lista;
        }

        public void Create(Reserva e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"Exec AddReserva @dia, @turno, @bloco, @idSala, @idAdmin, @idProfessor, @idTurma";

            cmd.Parameters.AddWithValue("@dia", e.Data);
            cmd.Parameters.AddWithValue("@turno", e.Turno);
            cmd.Parameters.AddWithValue("@bloco", e.Bloco);
            cmd.Parameters.AddWithValue("@idSala", e.Sala.Id);
            cmd.Parameters.AddWithValue("@idAdmin", e.Admin.Id);
            cmd.Parameters.AddWithValue("@idProfessor", e.Professor.Id);
            cmd.Parameters.AddWithValue("@idTurma", e.Turma.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"UPDATE reservas SET estatus = 2 WHERE id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }

        public bool CheckReserva(int idSala, string Dia, int Turno, int Bloco)
        {
            bool e = false;

            //select * from v_reservas where idSala = 2 and Estatus = 1 and Dia = 2 and Turno = 1 and Bloco = 1
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_reservas where idSala = @idSala and Estatus = 1 and Dia = @dia and Turno = @turno and Bloco = @bloco";

            cmd.Parameters.AddWithValue("@idSala", idSala);
            cmd.Parameters.AddWithValue("@dia", Dia);
            cmd.Parameters.AddWithValue("@turno", Turno);
            cmd.Parameters.AddWithValue("@bloco", Bloco);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                e = true;
            }

            return e;
        }
    }
}