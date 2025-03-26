using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Exemplo4_Exercicio.models;

namespace Exemplo4_Exercicio.database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Software> Softwares { get; set; }
    }
}