﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IContabilidadeRepository
    {
        int Inserir(Contabilidade contabilidade);

        List<Contabilidade> ObterTodos();

        bool Update(Contabilidade contabilidade);

        Contabilidade ObterPeloId(int id);

        bool Delete(int id);
    }
}
