using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface ICartaoCreditoRepository
    {
        int Inserir(CartaoCredito CartaoCredito);

        List<CartaoCredito> ObterTodos();

        bool Alterar(CartaoCredito CartaoCredito);

        CartaoCredito ObterPeloId(int id);

        bool Apagar(int id);
    }
}
