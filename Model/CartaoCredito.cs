﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CartaoCredito
    {
        public int Id;

        public Cliente Cliente;
        public int IdCliente;

        public string Numero;
        public DateTime DataVencimento;
        public string Cvv;
    }
}
