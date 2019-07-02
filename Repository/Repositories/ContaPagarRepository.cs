using Model;
using Repository.DataBase;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public List<ContaPagar> ObterTodos(string pesquisa)
        {
            throw new NotImplementedException();
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
