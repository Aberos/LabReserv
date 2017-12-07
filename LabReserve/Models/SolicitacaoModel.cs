using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class SolicitacaoModel : ModelBase
    {
        public List<Solicitacao> Read()
        {

            List<Solicitacao> Lista = new List<Solicitacao>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_solicitacao where Status <> 4";

            //cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Solicitacao e = new Solicitacao();
                e.idSolicitacao = (int)reader["idSolicitacao"];
                e.idProfessor = (int)reader["idProfessor"];
                e.nomeProfessor = (string)reader["nomeProfessor"];
                e.sobrenomeProfessor = (string)reader["sobrenomeProfessor"];
                e.idSala = (int)reader["idSala"];
                e.nomeSala = (string)reader["nomeSala"];
                e.idTurma = (int)reader["idTurma"];
                e.nomeTurma = (string)reader["nomeTurma"];
                e.idCurso = (int)reader["idCurso"];
                e.nomeCurso = (string)reader["nomeCurso"];
                e.Dia = (int)reader["Dia"];
                e.Turno = (int)reader["Turno"];
                e.Bloco = (int)reader["Bloco"];
                e.Status = (int)reader["Status"];

                Lista.Add(e);
            }

            return Lista;

        }


        public List<Solicitacao> Read(int id)
        {
            List<Solicitacao> Lista = new List<Solicitacao>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_solicitacao where idProfessor = @id and Status <> 4";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Solicitacao e = new Solicitacao();
                e.idSolicitacao = (int)reader["idSolicitacao"];
                e.idProfessor = (int)reader["idProfessor"];
                e.nomeProfessor = (string)reader["nomeProfessor"];
                e.sobrenomeProfessor = (string)reader["sobrenomeProfessor"];
                e.idSala = (int)reader["idSala"];
                e.nomeSala = (string)reader["nomeSala"];
                e.idTurma = (int)reader["idTurma"];
                e.nomeTurma = (string)reader["nomeTurma"];
                e.idCurso = (int)reader["idCurso"];
                e.nomeCurso = (string)reader["nomeCurso"];
                e.Dia = (int)reader["Dia"];
                e.Turno = (int)reader["Turno"];
                e.Bloco = (int)reader["Bloco"];
                e.Status = (int)reader["Status"];

                Lista.Add(e);
            }

            return Lista;
        }


        public void Create(Solicitacao e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"insert into solicitacoes values (@idProfessor, @idSala, @idTurma, @dia, @turno, @bloco, 1)";

            cmd.Parameters.AddWithValue("@idProfessor", e.idProfessor);
            cmd.Parameters.AddWithValue("@idSala", e.idSala);
            cmd.Parameters.AddWithValue("@idTurma", e.idTurma);

            cmd.Parameters.AddWithValue("@dia", e.Dia);
            cmd.Parameters.AddWithValue("@turno", e.Turno);
            cmd.Parameters.AddWithValue("@bloco", e.Bloco);

            cmd.ExecuteNonQuery();
        }

        public void UpdateStatus(Solicitacao e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"update solicitacoes set status = @status where id = @id";

            cmd.Parameters.AddWithValue("@status", e.Status);
            cmd.Parameters.AddWithValue("@id", e.idSolicitacao);

            cmd.ExecuteNonQuery();
        }
    }
}