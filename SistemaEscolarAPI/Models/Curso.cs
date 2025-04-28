using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }


        // De acordo com diagrama inicial, isso não recomendado, porém eu 
        // public ICollection<Aluno> Alunos { get; set; }
        // public ICollection<Disciplina> Disciplina { get; set; }

         public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }
    }
}