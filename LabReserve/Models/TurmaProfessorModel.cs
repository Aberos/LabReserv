using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class TurmaProfessorModel : ModelBase
    {
        public List<TurmaProfessor> Read(int id)
        {
            List<TurmaProfessor> lista = new List<TurmaProfessor>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_turma_prof where idProfessor = @id and Status = 1";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TurmaProfessor e = new TurmaProfessor();
                e.idTurma = (int)reader["idTurma"];
                e.nomeTurma = (string)reader["nomeTurma"];
                e.idCurso = (int)reader["idCurso"];
                e.nomeCurso = (string)reader["nomeCurso"];
                e.Status = (int)reader["Status"];

                lista.Add(e);
            }


            return lista;
        }

        public void Add(int idProfessor, int idTurma)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"Exec AddProfessorTurma @idProfessor, @idTurma";

            cmd.Parameters.AddWithValue("@idProfessor", idProfessor);
            cmd.Parameters.AddWithValue("@idTurma", idTurma);

            cmd.ExecuteNonQuery();
        }

        public void Remove(int idProfessor, int idTurma)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"UPDATE turma_professor SET status = 2 WHERE id_professor = @idProfessor and id_turma = @idTurma";

            cmd.Parameters.AddWithValue("@idProfessor", idProfessor);
            cmd.Parameters.AddWithValue("@idTurma", idTurma);

            cmd.ExecuteNonQuery();
        }
    }
}