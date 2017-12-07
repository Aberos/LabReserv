﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class ProfessorModel : ModelBase
    {
        public void Create(Professor e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"Exec CadProfessor @estatus, @email, @nome, @sobreNome, @cel, @senha";

            cmd.Parameters.AddWithValue("@estatus", e.Status);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@sobreNome", e.Sobrenome);
            cmd.Parameters.AddWithValue("@cel", e.Celular);
            cmd.Parameters.AddWithValue("@senha", e.Senha);

            cmd.ExecuteNonQuery();
        }

        public Professor Read(string email, string senha)
        {
            Professor e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Exec LogarProfessor @email, @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                e = new Professor();
                e.Id = (int)reader["idProfessor"];
                e.Nome = (string)reader["Nome"];
                e.Sobrenome = (string)reader["Sobrenome"];
                e.Email = (string)reader["Email"];
                e.Senha = (string)reader["Senha"];
                e.Celular = (string)reader["Celular"];
                e.Status = (int)reader["Status"];
                e.Permissao = (int)reader["Permissao"];
            }

            return e;
        }

        public List<Professor> Read()
        {
            List<Professor> lista = new List<Professor>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_professores";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Professor e = new Professor();
                e.Id = (int)reader["idProfessor"];
                e.Nome = (string)reader["Nome"];
                e.Sobrenome = (string)reader["Sobrenome"];
                e.Email = (string)reader["Email"];
                e.Senha = (string)reader["Senha"];
                e.Celular = (string)reader["Celular"];
                e.Status = (int)reader["Status"];
                e.Permissao = (int)reader["Permissao"];

                lista.Add(e);
            }

            return lista;
        }

        public Professor Read(int id)
        {
            Professor e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_professores where idProfessor = @id";

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                e = new Professor();
                e.Id = (int)reader["idProfessor"];
                e.Nome = (string)reader["Nome"];
                e.Sobrenome = (string)reader["Sobrenome"];
                e.Email = (string)reader["Email"];
                e.Celular = (string)reader["Celular"];
                e.Status = (int)reader["Status"];
                e.Permissao = (int)reader["Permissao"];
            }

            return e;
        }


        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"UPDATE pessoas SET estatus = 2 WHERE id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}