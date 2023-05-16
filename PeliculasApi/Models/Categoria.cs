using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required] // Poner campos Not Null en la BD
        public string Nombre { get; set; }
        [Required] //Para que al crear una Migracion No salgan los campos como que se pueden dejar NULL
        public  bool Estado { get; set; }
    }
}
