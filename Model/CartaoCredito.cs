using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartaoCredito
    {
        public int Id;

        public Contabilidade Contabilidade;
        public int IdContabilidade;

        public string Nome;
        public string Cpf;
    }
}
