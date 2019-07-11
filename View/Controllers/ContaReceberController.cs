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

        public ActionResult Store(int idCategoria, int idCliente, string nome, DateTime dataPagamento, decimal valor)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.IdCategoria = idCategoria;
            contaReceber.IdCliente = idCliente;
            contaReceber.Nome = nome;
            contaReceber.DataPagamento = dataPagamento;
            contaReceber.Valor = valor;
            repositorio.Inserir(contaReceber);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ContaReceber contaReceber = repositorio.ObterPeloId(id);
            ViewBag.ContaReceber = contaReceber;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos();
            ViewBag.Clientes = clientes;

            return View();
        }

        public ActionResult Update(int id, int idCliente, int idCategoria, string nome, DateTime dataPagamento, decimal valor)
        {
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = id;
            contaReceber.Nome = nome;
            contaReceber.DataPagamento = dataPagamento;
            contaReceber.IdCategoria = idCategoria;
            contaReceber.IdCliente = idCliente;
            contaReceber.Valor = valor;
            repositorio.Alterar(contaReceber);

            return RedirectToAction("Index");
        }

    }
}