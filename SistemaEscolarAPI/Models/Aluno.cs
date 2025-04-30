using System.Collections.Generic;
namespace SistemaEscolarAPI.Models;


public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int CursoId { get; set; }
    public Curso Curso { get; set; }

    // Icollection de disciplinas que o aluno está matriculado
    // e o curso que ele está matriculado
    public ICollection<DisciplinaAlunoCurso> DisciplinasAlunoCurso { get; set; }
}
