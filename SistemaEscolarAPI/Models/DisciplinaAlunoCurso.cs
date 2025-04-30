using System.Collections.Generic;
namespace SistemaEscolarAPI.Models;



public class DisciplinaAlunoCurso
{
    public int AlunoID { get; set; }
    public Aluno Aluno { get; set; }

    public int CursoId { get; set; }
    public Curso Curso { get; set; }

    public int DisciplinaId { get; set; }
    public Disciplina Disciplina { get; set; }
}
