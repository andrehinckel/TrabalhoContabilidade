using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Compra
    {
        public int Id;
        public decimal Valor;
        public DateTime DataCompra;

        public int IdCartaoCredito;
        public CartaoCredito CartaoCredito;

    }
}
