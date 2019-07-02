using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class CartaoCreditoRepository : ICartaoCreditoRepository
    {
        public bool Alterar(CartaoCredito CartaoCredito)
        {
            throw new NotImplementedException();
        }

        public bool Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(CartaoCredito CartaoCredito)
        {
            throw new NotImplementedException();
        }

        public CartaoCredito ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<CartaoCredito> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
