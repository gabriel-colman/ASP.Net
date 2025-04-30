using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaEscolarAPI.DTOs
{
    public class DisciplinaAlunoCursoDTO
    {
        public int AlunoId { get; set; }
        public int DisciplinaId { get; set; }
        public int CursoId { get; set; }
    }
}