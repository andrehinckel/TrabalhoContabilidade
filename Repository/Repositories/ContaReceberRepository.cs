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
    public class ContaReceberRepository : IContaReceberRepository
    {
        public bool Alterar(ContaReceber contaReceber)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public ContaReceber ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<ContaReceber> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
