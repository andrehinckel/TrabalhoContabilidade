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
    public class ClienteRepository : IClienteRepository
    {
        public bool Alterar(Cliente cliente)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"UPDATE clientes SET nome = @NOME, id_contabilidade = @ID_CONTABILIDADE, cpf = @CPF WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", cliente.Id);
            command.Parameters.AddWithValue("@NOME", cliente.Nome);
            command.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);
            command.Parameters.AddWithValue("@CPF", cliente.Cpf);
            int quantidade = command.ExecuteNonQuery();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand comand = Connection.OpenConnection();
            comand.CommandText = "DELETE FROM clientes WHERE id = @ID";
            comand.Parameters.AddWithValue("@ID", id);
            int quantidade = comand.ExecuteNonQuery();
            comand.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(Cliente cliente)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"INSERT INTO clientes (id_contabilidade, nome, cpf) OUTPUT INSERTED.ID VALUES (@ID_CONTABILIDADE, @NOME, @CPF)";
            command.Parameters.AddWithValue("@ID_CONTABILIDADE", cliente.IdContabilidade);
            command.Parameters.AddWithValue("@NOME", cliente.Nome);
            command.Parameters.AddWithValue("@CPF", cliente.Cpf);
            int id = Convert.ToInt32(command.ExecuteScalar());
            return id;
        }

        public Cliente ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "SELECT * FROM clientes WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();

            if(table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Cliente cliente = new Cliente();
            cliente.Id = Convert.ToInt32(row["id"]);
            cliente.IdContabilidade = Convert.ToInt32(row["id_contabilidade"]);
            cliente.Nome = row["nome"].ToString();
            cliente.Cpf = row["cpf"].ToString();
            return cliente;
        }

        public List<Cliente> ObterTodos(string pesquisa)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT
contabilidades.id AS 'ContabilidadeId',
contabilidades.nome AS 'ContabilidadeNome',
clientes.id AS 'Id',
clientes.nome AS 'Nome',
clientes.cpf AS 'Cpf'
FROM clientes
INNER JOIN contabilidades ON(clientes.id_contabilidade = contabilidades.id)";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            List<Cliente> clientes = new List<Cliente>();

            foreach(DataRow row in table.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Id = Convert.ToInt32(row["Id"]);
                cliente.Nome = row["Nome"].ToString();
                cliente.Cpf = row["Cpf"].ToString();
                cliente.IdContabilidade = Convert.ToInt32(row["ContabilidadeId"]);

                cliente.Contabilidade = new Contabilidade();
                cliente.Contabilidade.Id = Convert.ToInt32(row["ContabilidadeId"]);
                cliente.Contabilidade.Nome = row["ContabilidadeNome"].ToString();
                clientes.Add(cliente);
            }
            return clientes;
        }
    }
}
