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
    public class EventosController : Controller
    {
        private Banco db;
        private IHostingEnvironment env;

        private String UploadImagem(IFormFile formFile)
        {
            if(formFile != null || formFile.Length != 0)
            {
                string extension = Path.GetExtension(formFile.FileName);
                string fileName = $"{Guid.NewGuid().ToString()}{extension}";
                var path = Path.Combine(env.WebRootPath, "eventos", fileName).ToLower();

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                return $"/eventos/{fileName}";
            }

            return null;
        }

        public EventosController(Banco _db, IHostingEnvironment _env)
        {
            this.env = _env;
            this.db = _db;
        }

        public IActionResult Index()
        {
            List<Evento> eventos = this.db.Eventos.ToList();
            return View(eventos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            EventoVM vm = new EventoVM();

            var cursos = db.Cursos.ToList();
            foreach(var curso in cursos)
            {
                vm.Cursos.Add(new SelectListItem
                {
                    Value = curso.Id.ToString(),
                    Text = curso.Nome
                });
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(EventoVM vm)
        {
            if(ModelState.IsValid)
            {
                Evento evento = new Evento();
                evento.Nome = vm.Nome;
                evento.Descricao = vm.Descricao;
                evento.CaminhoImagem = this.UploadImagem(vm.Imagem);
                evento.Curso = db.Cursos.Find(vm.IdCursoSelecionado);
                this.db.Eventos.Add(evento);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }

            var cursos = db.Cursos.ToList();
            foreach (var curso in cursos)
            {
                vm.Cursos.Add(new SelectListItem
                {
                    Value = curso.Id.ToString(),
                    Text = curso.Nome
                });
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            Evento evento = this.db.Eventos
                                   .Include(m => m.Curso)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefault();

            if (evento == null)
            {
                return NotFound();
            }

            EventoVM vm = new EventoVM();
            vm.Nome = evento.Nome;
            vm.Descricao = evento.Descricao;
            var cursos = db.Cursos.ToList();
            foreach (var curso in cursos)
            {
                vm.Cursos.Add(new SelectListItem
                {
                    Value = curso.Id.ToString(),
                    Text = curso.Nome
                });
            }
            vm.IdCursoSelecionado = evento.Curso.Id;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(long id, EventoVM vm)
        {
            if (ModelState.IsValid)
            {
                Evento eventoDb = this.db.Eventos.Find(id);
                eventoDb.Nome = vm.Nome;
                eventoDb.Descricao = vm.Descricao;
                eventoDb.CaminhoImagem = this.UploadImagem(vm.Imagem);
                eventoDb.Curso = db.Cursos.Find(vm.IdCursoSelecionado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HttpGet]
        public IActionResult Detail(long id)
        {
            Evento evento = this.db.Eventos
                                  .Include(m => m.Curso)
                                  .Where(x => x.Id == id)
                                  .FirstOrDefault();

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            Evento evento = this.db.Eventos
                                  .Include(m => m.Curso)
                                  .Where(x => x.Id == id)
                                  .FirstOrDefault();

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        [HttpPost]
        public IActionResult Delete(long id, Evento evento)
        {
            Evento eventoDb = this.db.Eventos
                                  .Include(m => m.Curso)
                                  .Where(x => x.Id == id)
                                  .FirstOrDefault();

            db.Eventos.Remove(eventoDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}