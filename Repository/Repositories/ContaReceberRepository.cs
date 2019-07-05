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
    public class ContaReceberRepository : IContaReceberRepository
    {
        public bool Alterar(ContaReceber contaReceber)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"UPDATE contas_receber SET 
id_cliente = @ID_CLIENTE, 
id_categoria = @ID_CATEGORIA, 
nome = @NOME, 
data_pagamento = @DATA_PAGAMENTO, 
valor = @VALOR WHERE id = @ID";
            command.Parameters.AddWithValue("@ID_CLIENTE", contaReceber.IdCliente);
            command.Parameters.AddWithValue("@ID_CATEGORIA", contaReceber.IdCategoria);
            command.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            command.Parameters.AddWithValue("@DATA_PAGAMENTO", contaReceber.DataPagamento);
            command.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            command.Parameters.AddWithValue("@ID", contaReceber.Id);
            int quantidade = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidade == 1;
        }

        public bool Apagar(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "DELETE FROM contas_receber WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidade = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(ContaReceber contaRecber)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"INSERT INTO contas_receber (id_cliente, id_categoria, nome, data_pagamento, valor) OUTPUT INSERTED.ID VALUES (@ID_CLIENTE, @ID_CATEGORIA, @NOME, @DATA_PAGAMENTO, @VALOR)";
            command.Parameters.AddWithValue("@ID_CLIENTE", contaRecber.IdCliente);
            command.Parameters.AddWithValue("@ID_CATEGORIA", contaRecber.IdCategoria);
            command.Parameters.AddWithValue("@NOME", contaRecber.Nome);
            command.Parameters.AddWithValue("@DATA_PAGAMENTO", contaRecber.DataPagamento);
            command.Parameters.AddWithValue("@VALOR", contaRecber.Valor);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public ContaReceber ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT * FROM contas_receber WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();

            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(row["id"]);
            contaReceber.IdCliente = Convert.ToInt32(row["id_cliente"]);
            contaReceber.IdCategoria = Convert.ToInt32(row["id_categoria"]);
            contaReceber.Nome = row["nome"].ToString();
            contaReceber.DataPagamento = Convert.ToDateTime(row["data_pagamento"]);
            contaReceber.Valor = Convert.ToDecimal(row["valor"]);
            return contaReceber;


        }

        public List<ContaReceber> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT
categorias.id AS 'CategoriaId',
categorias.nome AS 'CategoriaNome',

clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',

contas_receber.id AS 'Id',
contas_receber.nome AS 'Nome',
contas_receber.data_pagamento AS 'DataPagamento',
contas_receber.valor AS 'Valor'
FROM contas_receber
INNER JOIN categorias ON(contas_receber.id_categoria = categorias.id)
INNER JOIN clientes ON(contas_receber.id_cliente = clientes.id)";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();

            List<ContaReceber> contasReceber = new List<ContaReceber>();

            foreach(DataRow row in table.Rows)
            {
                ContaReceber contaReceber = new ContaReceber();

                contaReceber.IdCategoria = Convert.ToInt32(row["CategoriaId"]);     
                contaReceber.IdCliente = Convert.ToInt32(row["ClienteId"]);
                contaReceber.Id = Convert.ToInt32(row["Id"]);
                contaReceber.Nome = row["Nome"].ToString();
                contaReceber.DataPagamento = Convert.ToDateTime(row["DataPagamento"]);
                contaReceber.Valor = Convert.ToDecimal(row["Valor"]);

                contaReceber.Categoria = new Categoria();
                contaReceber.Categoria.Id = Convert.ToInt32(row["CategoriaId"]);
                contaReceber.Categoria.Nome = row["CategoriaNome"].ToString();

                contaReceber.Cliente = new Cliente();
                contaReceber.Cliente.Id = Convert.ToInt32(row["ClienteId"]);
                contaReceber.Cliente.Nome = row["ClienteNome"].ToString();
                contaReceber.Cliente.Cpf = row["ClienteCpf"].ToString();
                contasReceber.Add(contaReceber);
            }
            return contasReceber;
        }
    }
}
