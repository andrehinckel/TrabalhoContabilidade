using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class ContaPagarController : Controller
    {
        private ContaPagarRepository repository;

        // GET: ContaPagar
        public ActionResult Index()
        {
            return View();
        }
    }
}