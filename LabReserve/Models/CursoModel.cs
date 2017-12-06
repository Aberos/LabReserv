using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class CursoModel : ModelBase
    {
        public List<Curso> Read()
        {
            List<Curso> lista = new List<Curso>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from cursos";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Curso e = new Curso();
                e.Id = (int)reader["id"];
                e.Nome = (string)reader["nome"];

                lista.Add(e);
            }

            return lista;
        }


        public void Create(Curso e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"insert into cursos values (@nome)";

            cmd.Parameters.AddWithValue("@nome", e.Nome);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection; // objeto herdado do ModelBase
            cmd.CommandText = @"delete from cursos where id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
    }
}