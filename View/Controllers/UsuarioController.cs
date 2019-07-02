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

        public ActionResult Cadastro()
        {
            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;
            return View();
        }

        public ActionResult Store(string login, string senha, DateTime dataNascimento, int idContabilidade)
        {
            Usuario usuario = new Usuario();
            usuario.Login = login;
            usuario.Senha = senha;
            usuario.DataNascimento = dataNascimento;
            usuario.IdContabilidade = idContabilidade;
            repository.Inserir(usuario);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Usuario usuario = repository.ObterPeloId(id);
            ViewBag.Usuario = usuario;

            ContabilidadeRepository contabilidadeRepository = new ContabilidadeRepository();
            List<Contabilidade> contabilidades = contabilidadeRepository.ObterTodos();
            ViewBag.Contabilidades = contabilidades;

            return View();
        }

        public ActionResult Update(int id, string login, string senha, DateTime dataNascimento, int idContabilidade)
        {
            Usuario usuario = new Usuario();
            usuario.Id = id;
            usuario.Login = login;
            usuario.Senha = senha;
            usuario.DataNascimento = dataNascimento;
            usuario.IdContabilidade = idContabilidade;
            repository.Update(usuario);
            return RedirectToAction("Index");
        }
    }
}