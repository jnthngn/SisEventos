using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisEventos.Models
{
    public class Banco : DbContext
    {
        public Banco(DbContextOptions<Banco> options) : base(options) { }

        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        
    }
}
