using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SisEventos.Models;

namespace SisEventos.Controllers
{
    public class HomeController : Controller
    {

        private Banco db;

        public HomeController(Banco _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            List<Evento> eventos = db.Eventos
                                     .OrderByDescending(x => x.Id)
                                     .Take(3)
                                     .ToList();
            return View(eventos);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
