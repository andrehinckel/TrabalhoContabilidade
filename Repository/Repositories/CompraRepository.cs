﻿using Model;
using Repository.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
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

        public Compra ObterPeloId(int id)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT * FROM compras WHERE id = @ID";
            command.Parameters.AddWithValue("@id", id);
            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            command.Connection.Close();
            if (table.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = table.Rows[0];
            Compra compra = new Compra();
            compra.Id = Convert.ToInt32(row["id"]);
            compra.IdCartaoCredito = Convert.ToInt32(row["id_cartao_credito"]);
            compra.Valor = Convert.ToDecimal(row["valor"]);
            compra.DataCompra = Convert.ToDateTime(row["data_compra"]);

            return compra;
        }

        public List<Compra> ObterTodos()
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"SELECT 
cartoes_credito.id AS 'CartaoCreditoId',
cartoes_credito.numero AS 'CartaoCreditoNumero',
cartoes_credito.data_vencimento AS 'CartaoCreditoDataVencimento',
cartoes_credito.cvv AS 'CartaoCreditoCvv',
compras.id AS 'Id',
compras.id_cartao_credito AS 'IdCartaoCredito',
compras.valor AS 'Valor',
compras.data_compra AS 'DataCompra'
FROM compras 
INNER JOIN cartoes_credito ON(compras.id_cartao_credito = cartoes_credito.id)";

            DataTable table = new DataTable();
            table.Load(command.ExecuteReader());
            List<Compra> compras = new List<Compra>();
            command.Connection.Close();

            foreach(DataRow row in table.Rows)
            {
                Compra compra = new Compra();
                compra.Id = Convert.ToInt32(row["Id"]);
                compra.IdCartaoCredito = Convert.ToInt32(row["IdCartaoCredito"]);
                compra.Valor = Convert.ToDecimal(row["Valor"]);
                compra.DataCompra = Convert.ToDateTime(row["DataCompra"]);

                compra.CartaoCredito = new CartaoCredito();
                compra.CartaoCredito.Id = Convert.ToInt32(row["CartaoCreditoId"]);
                compra.CartaoCredito.Numero = row["CartaoCreditoNumero"].ToString();
                compra.CartaoCredito.DataVencimento = Convert.ToDateTime(row["CartaoCreditoDataVencimento"]);
                compra.CartaoCredito.Cvv = row["CartaoCreditoCvv"].ToString();
                compras.Add(compra);
            }
            return compras;
            
        }

        public bool Update(Compra compra)
        {
            SqlCommand command = Connection.OpenConnection();
            command.CommandText = @"UPDATE compras SET id_cartao_credito = @ID_CARTAO_CREDITO, valor = @VALOR, data_compra = @DATA_COMPRA WHERE id = @ID";
            command.Parameters.AddWithValue("@ID_CARTAO_CREDITO", compra.IdCartaoCredito);
            command.Parameters.AddWithValue("@VALOR", compra.Valor);
            command.Parameters.AddWithValue("@DATA_COMPRA", compra.DataCompra);
            command.Parameters.AddWithValue("@ID", compra.Id);
            int quantidadeAfetada = command.ExecuteNonQuery();
            command.Connection.Close();
            return quantidadeAfetada == 1;
        }
    }
}
