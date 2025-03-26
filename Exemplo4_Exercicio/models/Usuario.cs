using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace Exemplo4_Exercicio.models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Column("ID_Usuario")]
        public int Id { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Nome_Usuario")]
        public string Nome { get; set; }

        [Column("Ramal")]
        public int Ramal { get; set; }

        [Column("Especialidade")]
        public string Especialidade { get; set; }
    }
}