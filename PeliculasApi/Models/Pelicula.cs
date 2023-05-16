using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeliculasApi.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string NombrePelicula { get; set; }
        // Este va a ser el ForenKey con la Tabla Categoria
        [Required]
        public int CategoriaId { get; set; }
        // Para relacionarlo Tenemos que crear otra propiedad
        // de tipo Categoria en este caso entre los () se le pasa
        // el mismo nombre de la varaible de arriba 'CategoriaId' para relacionarlo
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        public string Director { get; set; }

    }
}
