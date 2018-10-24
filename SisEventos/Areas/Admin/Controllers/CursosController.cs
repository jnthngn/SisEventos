using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SisEventos.Models;
using SisEventos.ViewModels;

namespace SisEventos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CursosController : Controller
    {
        private Banco db;
        private IHostingEnvironment env;

        public CursosController(Banco _db, IHostingEnvironment _env)
        {
            this.env = _env;
            this.db = _db;
        }

        public IActionResult Index()
        {
            List<Curso> cursos = this.db.Cursos.ToList();
            return View(cursos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CursoVM vm = new CursoVM();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CursoVM vm)
        {
            if (ModelState.IsValid)
            {
                Curso curso = new Curso();
                curso.Nome = vm.Nome;
                curso.Descricao = vm.Descricao;
                this.db.Cursos.Add(curso);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            Curso curso = this.db.Cursos
                                   
                                   .Where(x => x.Id == id)
                                   .FirstOrDefault();

            if (curso == null)
            {
                return NotFound();
            }

            CursoVM vm = new CursoVM();
            vm.Nome = curso.Nome;
            vm.Descricao = curso.Descricao;
            var cursos = db.Cursos.ToList();
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(long id, CursoVM vm)
        {
            if (ModelState.IsValid)
            {
                Curso cursoDb = this.db.Cursos.Find(id);
                cursoDb.Nome = vm.Nome;
                cursoDb.Descricao = vm.Descricao;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            Curso curso = this.db.Cursos
                                  .Where(x => x.Id == id)
                                  .FirstOrDefault();

            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            Curso curso = this.db.Cursos
                                
                                  .Where(x => x.Id == id)
                                  .FirstOrDefault();

            if (curso == null)
            {
                return NotFound();
            }

            return View(curso);
        }

        [HttpPost]
        public IActionResult Delete(long id, Curso curso)
        {
            Curso cursoDb = this.db.Cursos
                                  
                                  .Where(x => x.Id == id)
                                  .FirstOrDefault();

            db.Cursos.Remove(cursoDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}