using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exemplo4_Exercicio.models
{
    [Table("Maquina")]
    public class Maquina
    {
         [Column("Id_Maquina")]
        public int Id { get; set; }

        [Column("Tipo")]
        public string Tipo { get; set; }

        [Column("Velocidade")]
        public int Velocidade { get; set; }

        [Column("HardDisk")]
        public int HardDisk { get; set; }

        [Column("Placa_Rede")]
        public int PlacaRede { get; set; }

        [Column("Memoria_Ram")]
        public int MemoriaRam { get; set; }

        [Column("Fk_Usuario")]
        public int FkUsuario { get; set; }
    }
}