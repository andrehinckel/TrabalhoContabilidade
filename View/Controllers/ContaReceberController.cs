using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaReceberController : Controller
    {
        private ContaReceberRepository repositorio;

        public ContaReceberController()
        {
            repositorio = new ContaReceberRepository();
        }

        // GET: ContaReceber
        public ActionResult Index()
        {
            List<ContaReceber> contasReceber = repositorio.ObterTodos();
            ViewBag.ContasReceber = contasReceber;
            return View();
        }

        public ActionResult Cadastro()
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos();
            ViewBag.Clientes = clientes;

            return View();
        }

        /*public ActionResult Store(int idCategoria, int idCliente, string nome, DateTime data_pagamento decimal valor)
        {

        }*/
    }
}