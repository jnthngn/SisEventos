using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SisEventos.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SisEventos.ViewModels
{
    public class EventoVM
    {
        public EventoVM()
        {
            this.Cursos = new List<SelectListItem>();
        }

        public long Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do evento")]
        [Display(Name = "Nome do evento")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a descrição do evento")]
        [Display(Name = "Descrição do evento")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem de capa")]
        public IFormFile Imagem { get; set; }

        public List<SelectListItem> Cursos { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "Informe o curso do evento")]
        public long IdCursoSelecionado { get; set; }
    }
}
