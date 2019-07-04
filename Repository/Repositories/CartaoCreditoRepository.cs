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
    public class CartaoCreditoRepository : ICartaoCreditoRepository
    {
        public bool Alterar(CartaoCredito cartaoCredito)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"UPDATE FROM cartoes_credito SET id_cliente = @ID_CLIENTE, numero = @NUMERO, 
data_vencimanto = @DATA_VENCIMENTO, cvv = @CVV WHERE id = @ID";
            command.Parameters.AddWithValue("@ID_CLIENTE", cartaoCredito.IdCliente);
            command.Parameters.AddWithValue("@NUMERO", cartaoCredito.Numero);
            command.Parameters.AddWithValue("@DATA_VENCIMENTO", cartaoCredito.DataVencimento);
            command.Parameters.AddWithValue("@CVV", cartaoCredito.Cvv);
            int quantidade = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidade == 1;

        }

        public bool Apagar(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"DELETE FROM cartoes_credito WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidade = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidade == 1;
        }

        public int Inserir(CartaoCredito cartaoCredito)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"INSERT INTO cartoes_credito (id_cliente, numero, data_vencimento, cvv)  OUTPUT INSERTED.ID VALUES (@ID_CLIENTE, @NUMERO, @DATA_VENCIMENTO, @CVV)";
            command.Parameters.AddWithValue("@ID_CLIENTE", cartaoCredito.IdCliente);
            command.Parameters.AddWithValue("@NUMERO", cartaoCredito.Numero);
            command.Parameters.AddWithValue("@DATA_VENCIMENTO", cartaoCredito.DataVencimento);
            command.Parameters.AddWithValue("@CVV", cartaoCredito.Cvv);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;

        }

        public CartaoCredito ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT * FROM cartoes_credito WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();

            if(table.Rows.Count == 0)
            {
                return null;
            }
            DataRow row = table.Rows[0];
            CartaoCredito cartaoCredito = new CartaoCredito();

            return cartaoCredito;
        }

        public List<CartaoCredito> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
