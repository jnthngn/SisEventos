using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SisEventos.Models;

namespace SisEventos.Controllers
{
    public class CursosController : Controller
    {

        private Banco db;

        public CursosController(Banco _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            List<Curso> cursos = db.Cursos.ToList();
            return View(cursos);
        }
    }
}
