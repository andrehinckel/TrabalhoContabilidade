using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteRepository repositorio;

        public ClienteController()
        {
            repositorio = new ClienteRepository();
        }

        // GET: Cliente
        public ActionResult Index()
        {
            List<Cliente> clientes = repositorio.ObterTodos();
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Cadastro()
        {
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidade = contabilidades;
            return ViewBag();
        }

        public ActionResult Store(int idContabilidade, string nome, string cpf)
        {
            Cliente cliente = new Cliente();
            cliente.Nome = nome;
            cliente.Cpf = cpf;
            cliente.IdContabilidade = idContabilidade;
            repositorio.Inserir(cliente);
            return RedirectToAction("Index");

        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Cliente cliente = repositorio.ObterPeloId(id);
            ViewBag.Cliente = cliente;

            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidade = contabilidades;
            return View();
        }

        public ActionResult Update(int id, string nome, int idContabilidade, string cpf)
        {
            Cliente cliente = new Cliente();
            cliente.Id = id;
            cliente.Nome = nome;
            cliente.Cpf = cpf;
            cliente.IdContabilidade = idContabilidade;
            repositorio.Alterar(cliente);
            return RedirectToAction("Index");
        }
    }
}