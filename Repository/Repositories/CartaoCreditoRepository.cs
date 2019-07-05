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
            cartaoCredito.Id = Convert.ToInt32(row["id"]);
            cartaoCredito.IdCliente = Convert.ToInt32(row["id_cliente"]);
            cartaoCredito.Numero = row["numero"].ToString();
            cartaoCredito.DataVencimento = Convert.ToDateTime(row["data_vencimento"]);
            cartaoCredito.Cvv = row["cvv"].ToString();
            return cartaoCredito;
        }

        public List<CartaoCredito> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT
clientes.id AS 'ClienteId',
clientes.nome AS 'ClienteNome',
clientes.cpf AS 'ClienteCpf',

cartoes_credito.id AS 'Id',
cartoes_credito.numero AS 'Numero',
cartoes_credito.cvv AS 'Cvv',
cartoes_credito.data_vencimento AS 'DataVencimento'
FROM cartoes_credito
INNER JOIN clientes ON(cartoes_credito.id_cliente = clientes.id)";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            List<CartaoCredito> cartoesCredito = new List<CartaoCredito>();

            foreach(DataRow row in table.Rows)
            {
                CartaoCredito cartaoCredito = new CartaoCredito();
                cartaoCredito.Id = Convert.ToInt32(row["Id"]);
                cartaoCredito.Numero = row["Numero"].ToString();
                cartaoCredito.Cvv = row["Cvv"].ToString();
                cartaoCredito.DataVencimento = Convert.ToDateTime(row["DataVencimento"]);

                cartaoCredito.Cliente = new Cliente();
                cartaoCredito.Cliente.Id = Convert.ToInt32(row["ClienteId"]);
                cartaoCredito.Cliente.Nome = row["ClienteNome"].ToString();
                cartaoCredito.Cliente.Cpf = row["ClienteCpf"].ToString();
                cartoesCredito.Add(cartaoCredito);
            }
            return cartoesCredito;
        }
    }
}
