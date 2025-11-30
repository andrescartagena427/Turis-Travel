using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Turis_Travel.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [Column("ID_cliente")]
        public int ID_cliente { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        [Column("Cedula")]
        public string Cedula { get; set; }

        [Column("Telefono")]
        public string Telefono { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Fecha_registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
