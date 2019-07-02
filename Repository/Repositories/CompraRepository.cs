using Model;
using Repository.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CompraRepository
    {
        public bool Delete(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = "DELETE FROM compras WHERE id = @ID";
            command.Parameters.AddWithValue("@ID", id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }

        public int Inserir(Compra compra)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"INSERT INTO compras (id_cartao_credito, valor, data_compra)
OUTPUT INSERTED.ID VALUES(@ID_CARTAO_CREDITO, @VALOR, @DATA_COMPRA)";
            command.Parameters.AddWithValue("@ID_CARTAO_CREDITO", compra.IdCartaoCredito);
            command.Parameters.AddWithValue("@VALOR", compra.Valor);
            command.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
            int id = Convert.ToInt32(command.ExecuteScalar());
            command.Connection.Close();
            return id;

        }
    }
}
