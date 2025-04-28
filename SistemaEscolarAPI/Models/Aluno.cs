using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        // Icollection é uma coleção que pode ser usada para armazenar uma lista de objetos, de forma que possa ser manipulada. 
        // Assim é a coleção de disciplinas que o aluno está matriculado.
        public ICollection<DisciplinaAlunoCurso> DisciplinaAlunoCursos { get; set; }
    }
}