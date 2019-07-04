using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CartaoCreditoController : Controller
    {
        private CartaoCreditoRepository repositorio;

        public CartaoCreditoController()
        {
            repositorio = new CartaoCreditoRepository();
        }

        // GET: CartaoCredito
        public ActionResult Index()
        {
            List<CartaoCredito> cartoesCredito = repositorio.ObterTodos();
            ViewBag.CartoesCredito = cartoesCredito;
            return View();
        }

        public ActionResult Cadastro()
        {
            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos();
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Store(int idCliente, string numero, DateTime dataVencimento, string cvv)
        {
            CartaoCredito cartaoCredito = new CartaoCredito();
            cartaoCredito.IdCliente = idCliente;
            cartaoCredito.Numero = numero;
            cartaoCredito.DataVencimento = dataVencimento;
            cartaoCredito.Cvv = cvv;
            repositorio.Inserir(cartaoCredito);
            return RedirectToAction("Index");
        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            CartaoCredito cartaoCredito = repositorio.ObterPeloId(id);
            ViewBag.CartaoCredito = cartaoCredito;

            ClienteRepository clienteRepository = new ClienteRepository();
            List<Cliente> clientes = clienteRepository.ObterTodos();
            ViewBag.Clientes = clientes;
            return View();
        }

        public ActionResult Update(int id, int idCliente, string numero, DateTime dataVencimento, string cvv)
        {
            CartaoCredito cartaoCredito = new CartaoCredito();
            cartaoCredito.Id = id;
            cartaoCredito.IdCliente = idCliente;
            cartaoCredito.Numero = numero;
            cartaoCredito.Cvv = cvv;
            cartaoCredito.DataVencimento = dataVencimento;
            repositorio.Alterar(cartaoCredito);
            return RedirectToAction("Index");
        }
    }
}