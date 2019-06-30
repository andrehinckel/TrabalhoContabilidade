using Model;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ContabilidadeRepository : IContabilidadeRepository
    {
        public bool Alterar(Contabilidade contabilidade)
        {
            throw new NotImplementedException();
        }

        public bool Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Contabilidade contabilidade)
        {
            throw new NotImplementedException();
        }

        public Contabilidade ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contabilidade> ObterTodos()
        {
            throw new NotImplementedException();
        }
    }
}
