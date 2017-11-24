using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabReserve.Models
{
    public class SalaModel : ModelBase
    {
        public List<Sala> Read()
        {
            List<Sala> lista = new List<Sala>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"select * from salas";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Sala e = new Sala();
                e.Id = (int)reader["id"];
                e.Nome = (string)reader["nome"];

                lista.Add(e);
            }

            return lista;
        }
    }
}