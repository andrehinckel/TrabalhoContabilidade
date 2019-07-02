using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public bool Delete(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "DELETE FROM contabilidades WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(Categoria categoria)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "INSERT INTO categorias (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", categoria.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public Categoria ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT * FROM categorias WHERE id = @ID";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Categoria categoria = new Categoria();
            categoria.Id = Convert.ToInt32(row["id"]);
            categoria.Nome = row["nome"].ToString();
            return categoria;
        }

        public List<Categoria> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "SELECT * FROM categorias";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<Categoria> categorias = new List<Categoria>();
            command.Connection.Close();
            foreach (DataRow row in table.Rows)
            {
                Categoria categoria = new Categoria()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["nome"].ToString()
                };

                categorias.Add(categoria);
            }
            return categorias;
        }

        public bool Update(Categoria categoria)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "UPDATE categorias SET nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", categoria.Nome);
            command.Parameters.AddWithValue("@ID", categoria.Id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
