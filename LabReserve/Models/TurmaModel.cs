using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class TurmaModel : ModelBase
    {
        public List<Turma> Read()
        {
            List<Turma> lista = new List<Turma>();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_turmas";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Turma e = new Turma();
                e.Id = (int)reader["idTurma"];
                e.Nome = (string)reader["nomeTurma"];
                e.Curso.Id = (int)reader["idCurso"];
                e.Curso.Nome = (string)reader["nomeCurso"];

                lista.Add(e);
            }

            return lista;
        }



        public Turma Read(int id)
        {
            Turma e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_turmas where idTurma = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                e = new Turma();
                e.Id = (int)reader["idTurma"];
                e.Nome = (string)reader["nomeTurma"];
                e.Curso.Id = (int)reader["idCurso"];
                e.Curso.Nome = (string)reader["nomeCurso"];
            }

            return e;
        }


        public void Create(Turma e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"insert into turmas values (@nome, @id_curso)";

            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@id_curso", e.Curso.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"delete from turmas where id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}