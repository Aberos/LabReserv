using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class AdminModel : ModelBase
    {

        public void Create(Admin e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"Exec CadAdmin @estatus, @email, @nome, @sobreNome, @cel, @senha";

            cmd.Parameters.AddWithValue("@estatus", e.Status);
            cmd.Parameters.AddWithValue("@email", e.Email);
            cmd.Parameters.AddWithValue("@nome", e.Nome);
            cmd.Parameters.AddWithValue("@sobreNome", e.Sobrenome);
            cmd.Parameters.AddWithValue("@cel", e.Celular);
            cmd.Parameters.AddWithValue("@senha", e.Senha);

            cmd.ExecuteNonQuery();
        }

        public Admin Read(string email, string senha)
        {
            Admin e = null;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"Exec LogarAdmins @email, @senha";

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.Read())
            {
                e = new Admin();
                e.Id = (int)reader["idAdmin"];
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

        public List<Admin> Read()
        {
            List<Admin> lista = new List<Admin>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from v_admin";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Admin e = new Admin();
                e.Id = (int)reader["idAdmin"];
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
    }
}