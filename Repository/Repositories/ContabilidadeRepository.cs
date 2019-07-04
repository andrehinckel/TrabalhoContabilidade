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
    public class ContabilidadeRepository : IContabilidadeRepository
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

        public int Inserir(Contabilidade contabilidade)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "INSERT INTO contabilidades (nome) OUTPUT INSERTED.ID VALUES (@NOME)";
            command.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public Contabilidade ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "SELECT * FROM contabilidades WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Contabilidade contabilidade = new Contabilidade();
            contabilidade.Id = Convert.ToInt32(row["id"]);
            contabilidade.Nome = row["nome"].ToString();
            return contabilidade;
        }

        public List<Contabilidade> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "SELECT * FROM contabilidades";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<Contabilidade> contabilidades = new List<Contabilidade>();
            command.Connection.Close();
            foreach (DataRow row in table.Rows)
            {
                Contabilidade contabilidade = new Contabilidade()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Nome = row["nome"].ToString()
                };

                contabilidades.Add(contabilidade);

            }
            return contabilidades;
        }

        public bool Update(Contabilidade contabilidade)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "UPDATE contabilidades SET nome = @NOME WHERE id = @ID";
            command.Parameters.AddWithValue("@NOME", contabilidade.Nome);
            command.Parameters.AddWithValue("@ID", contabilidade.Id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
