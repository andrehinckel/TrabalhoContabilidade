using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IContaPagarRepository
    {
        int Inserir(ContaPagar contaPagar);

        bool Update(ContaPagar contaPagar);

        bool Delete(int id);

        List<ContaPagar> ObterTodos();

        ContaPagar ObterPeloId(int id);

    }
}
