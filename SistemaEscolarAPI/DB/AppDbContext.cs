using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;

namespace SistemaEscolarAPI.DB
{
    public class AppDbContext : DbContext
    {
        public DbSeet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<DisciplinaAlunoCurso> DisciplinasAlunosCursos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DisciplinaAlunoCurso>() // Configura a entidade DisciplinaAlunoCurso
                .HasKey(dc => new { dc.AlunoId, dc.DisciplinaId, dc.CursoId });
            // HasKey é usado para definir a chave primária composta da tabela de junção
        }
    }
}