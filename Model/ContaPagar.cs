using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContaPagar
    {
        public int Id;
        public string Nome;
        public decimal Valor;
        public DateTime DataVencimento;
        public DateTime DataPagamento;

        public int IdCliente;
        Cliente Cliente;

        public int IdCategoria;
        Categoria Categoria;
    }
}
