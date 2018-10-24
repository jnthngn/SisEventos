using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SisEventos.Models
{
    public class Curso
    {
        public long Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [Display(Name = "Descrição do curso")]
        public string Descricao { get; set; }
    }
}
