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
        public Cliente Cliente;

        public int IdCategoria;
        public Categoria Categoria;

        public string Nome;
        public DateTime DataPagamento;
        public decimal Valor;
        

    }
}
