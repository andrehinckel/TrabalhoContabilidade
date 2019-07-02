﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    interface IUsuarioRepository
    {
        int Inserir(Usuario usuario);

        bool Delete(int id);

        bool Update(Usuario usuario);

        List<Usuario> ObterTodos();

        Usuario ObterPeloId(int id);
    }
}
