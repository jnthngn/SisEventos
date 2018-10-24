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
    public class CursoVM
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do curso")]
        [Display(Name = "Nome do Curso")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Informe a descrição do curso")]
        [Display(Name = "Descrição do curso")]
        public string Descricao { get; set; }
    }
}
