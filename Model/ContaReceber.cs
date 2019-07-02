using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ContaReceber
    {
        public int Id;

        public int IdCliente;
        Cliente Cliente;

        public int IdCategoria;
        Categoria Categoria;

        public string Nome;
        public DateTime DataPagamento;
        public decimal Valor;
        

    }
}
