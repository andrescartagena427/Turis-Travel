using System.ComponentModel.DataAnnotations;

namespace Turis_Travel.Models
{
    public class Destino
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Ciudad { get; set; }

        [StringLength(200)]
        public string Pais { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [StringLength(500)]
        public string ImagenUrl { get; set; }
    }
}
