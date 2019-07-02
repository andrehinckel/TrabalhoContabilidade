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
    public class ContaPagarRepository : IContaPagarRepository
    {
        public bool Delete(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "DELETE FROM contas_pagar WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(ContaPagar contaPagar)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"INSERT INTO contas_pagar (id_cliente, id_categoria, nome, data_vencimento, data_pagamento, valor)
OUTPUT INSERTED.ID VALUES(@ID_CLIENTE, @ID_CATEGORIA, @NOME, @DATA_VENCIMENTO, @DATA_PAGAMENTO, @VALOR)";
            command.Parameters.AddWithValue("@ID_CLIENTE", contaPagar.IdCliente);
            command.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            command.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            command.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            command.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            command.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;
        }

        public ContaPagar ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT * FROM contas_pagar WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = Convert.ToInt32(row["id"]);
            contaPagar.IdCliente = Convert.ToInt32(row["id_cliente"]);
            contaPagar.IdCategoria = Convert.ToInt32(row["id_categoria"]);
            contaPagar.Nome = row["nome"].ToString();
            contaPagar.DataVencimento = Convert.ToDateTime(row["data_vencimento"]);
            contaPagar.DataPagamento = Convert.ToDateTime(row["data_pagamento"]);
            contaPagar.Valor = Convert.ToDecimal(row["valor"]);

            return contaPagar;
        }

        public List<ContaPagar> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT clientes.id AS 'ClienteId', clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',
categorias.id AS 'CategoriaId',
categorias.nome AS 'CategoriaNome',
contas_pagar.id AS 'Id',
contas_pagar.nome AS 'Nome',
contas_pagar.data_vencimento AS 'DataVencimento',
contas_pagar.data_pagamento AS 'DataPagamento',
contas_pagar.valor AS 'Valor' FROM contas_pagar INNER JOIN clientes ON(contas_pagar.id_cliente = clientes.id)
INNER JOIN categorias ON(contas_pagar.id_categoria = categorias.id)";
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<ContaPagar> ContasPagar = new List<ContaPagar>();
            command.Connection.Close();

            foreach(DataRow row in table.Rows)
            {
                ContaPagar contaPagar = new ContaPagar();
                contaPagar.Id = Convert.ToInt32(row["id"]);
                contaPagar.IdCliente = Convert.ToInt32(row["ClienteId"]);
                contaPagar.IdCategoria = Convert.ToInt32(row["CategoriaId"]);
                contaPagar.DataVencimento = Convert.ToDateTime(row["data_vencimento"]);
                contaPagar.DataPagamento = Convert.ToDateTime(row["data_pagamento"]);
                contaPagar.Nome = row["nome"].ToString();
                contaPagar.Valor = Convert.ToDecimal(row["valor"]);

                contaPagar.Cliente = new Cliente();
                contaPagar.Cliente.Id = Convert.ToInt32(row["ClienteId"]);
                contaPagar.Cliente.Nome = row["ClienteNome"].ToString();
                contaPagar.Cliente.Cpf = row["ClienteCpf"].ToString();

                contaPagar.Categoria = new Categoria();
                contaPagar.Categoria.Id = Convert.ToInt32(row["CategoriaId"]);
                contaPagar.Categoria.Nome = row["CategoriaNome"].ToString();
                ContasPagar.Add(contaPagar);
            }
            return ContasPagar;
        }

        public bool Update(ContaPagar contaPagar)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"UPDATE contas_pagar SET id_cliente = @ID_CLIENTE, id_categoria = @ID_CATEGORIA,
nome = @NOME, data_vencimento = @DATA_VENCIMENTO, data_pagamento = @DATA_PAGAMENTO, valor = @VALOR WHERE id = @ID";
            command.Parameters.AddWithValue("ID_CLIENTE", contaPagar.IdCliente);
            command.Parameters.AddWithValue("@ID_CATEGORIA", contaPagar.IdCategoria);
            command.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            command.Parameters.AddWithValue("@DATA_VENCIMENTO", contaPagar.DataVencimento);
            command.Parameters.AddWithValue("@DATA_PAGAMENTO", contaPagar.DataPagamento);
            command.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            command.Parameters.AddWithValue("@ID", contaPagar.Id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
