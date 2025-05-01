
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SistemaEscolarAPI.Models;

// para instalar os pacotes de entity framework core, use os seguintes comandos:
// dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
namespace SistemaEscolarAPI.DB;

public class AppDbContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<Disciplina> Disciplinas { get; set; }
    public DbSet<DisciplinaAlunoCurso> DisciplinaAlunoCurso { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder) // Aqui vai
    {
        modelBuilder.Entity<DisciplinaAlunoCurso>()
            .HasKey(x => new { x.AlunoId, x.CursoId, x.DisciplinaId }); // haskey é a chave primária composta

        // Relacionamentos adicionais
    }
}
