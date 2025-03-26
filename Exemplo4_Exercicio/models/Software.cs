using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;

namespace Exemplo4_Exercicio.models
{
    [Table("Software")]
    public class Software
    {
         [Column("Id_Software")]
        public int Id { get; set; }

        [Column("Produto")]
        public string Produto { get; set; }

        [Column("HardDisk")]
        public int HardDisk { get; set; }

        [Column("Memoria_Ram")]
        public int MemoriaRam { get; set; }

        [Column("Fk_Maquina")]
        public int FkMaquina { get; set; }
    }
}