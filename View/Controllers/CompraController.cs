using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CompraController : Controller
    {
        private CompraRepository repository;

        public CompraController()
        {
            repository = new CompraRepository();
        }

        // GET: Compra
        public ActionResult Index()
        {
            List<Compra> compras = repository.ObterTodos();
            ViewBag.Compras = compras;
            return View();
        }

        public ActionResult Cadastro()
        {
            CartaoCreditoRepository cartaoCreditoRepository = new CartaoCreditoRepository();
            List<CartaoCredito> cartoesCredito = cartaoCreditoRepository.ObterTodos();
            ViewBag.CartoesCredito = cartoesCredito;
            return View();
        }

        public ActionResult Store(int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Compra compra = new Compra();
            compra.IdCartaoCredito = idCartaoCredito;
            compra.Valor = valor;
            compra.DataCompra = dataCompra;
            repository.Inserir(compra);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Compra compra = repository.ObterPeloId(id);
            ViewBag.Compra = compra;

            CartaoCreditoRepository cartaoCreditoRepository = new CartaoCreditoRepository();
            List<CartaoCredito> cartoesCredito = cartaoCreditoRepository.ObterTodos();
            ViewBag.CartoesCredito = cartoesCredito;

            return View();
        }

        public ActionResult Update(int id, int idCartaoCredito, decimal valor, DateTime dataCompra)
        {
            Compra compra = new Compra();
            compra.Id = id;
            compra.IdCartaoCredito = idCartaoCredito;
            compra.Valor = valor;
            compra.DataCompra = dataCompra;
            repository.Update(compra);
            return RedirectToAction("Index");
        }
    }
}