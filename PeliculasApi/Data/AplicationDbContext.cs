using Microsoft.EntityFrameworkCore;
using PeliculasApi.Models;

namespace PeliculasApi.Data
{
    public class AplicationDbContext:DbContext
    {
        // La idea aqui que es la variable option es la cadena de conexion
        // que la toma DbContextOption para conectarse a la base de datos
        // que fue la configuracion agregada a appsettings.json
        public AplicationDbContext(DbContextOptions<AplicationDbContext> option): base(option)
        {
                
        }

        // Agregar los modelos que despues vamos a convertir en tablas

        // se le puso entre las <> el modelo que tenemos en la carpeta Models
        public DbSet<Categoria> Categorias { get; set; }
        // Agrega el nuevo modelo para que se cree como tabala al hacer la migracion
        public DbSet<Pelicula> Peliculas { get; set; }
    }
}

/*
 
 ctor doble tab para crear constructor
 prop doble tab para crear las propiedades
    
    ctrl + . para acceder a los errores o instala una dependencia

 */