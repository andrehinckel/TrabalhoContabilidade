using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository;

        public UsuarioController()
        {
            repository = new UsuarioRepository();
        }
        // GET: Usuario
        public ActionResult Index()
        {
            List<Usuario> usuarios = repository.ObterTodos();
            ViewBag.Usuarios = usuarios;
            return View();
        }


    }
}