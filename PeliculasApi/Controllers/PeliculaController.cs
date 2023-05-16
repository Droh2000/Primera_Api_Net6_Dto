using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Data;
using PeliculasApi.Models;
using System.ComponentModel;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        //Hacemos referencia a Db contenx para poder ejecutar las acciones
        private readonly AplicationDbContext _db;

        public PeliculaController(AplicationDbContext db)
        {
            _db = db;
        }

        // Metodos para obtener o Guardar datos en la Base de datos

        [HttpGet]
        public async Task<IActionResult> GetPeliculas()
        {
            // Queremos que nos retorne una lista ordenada por el nombre
            // de la pelicula y que tambien nos incluya la categoria a la que pretenesca
            // Como hay una relacion de modelo podemo hacer los de Include()
            // al final el toListAsync() nos lo manda en lista
            var lista = await _db.Peliculas.OrderBy(p => p.NombrePelicula).Include(p=> p.Categoria).ToListAsync();
            return Ok(lista);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetPelicula(int id) {
            // Queremos que por cada pelicula que encuentre nos traiga la categoria
            // Con el metodo 'FirstOrDefaultAsync' que se filtre por el id del parametro
            var obj = await _db.Peliculas.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (obj == null) { 
                return NotFound();
            }

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPelicula([FromBody] Pelicula pelicula)
        {
            //1- Se verifica que lo recibido no este vacio
            if (pelicula == null) {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid) { //Con ModelState evaluamos si no llenamos algun campo
                return BadRequest(ModelState);
            }

            await _db.AddAsync(pelicula);
            await _db.SaveChangesAsync();
            return Ok();// En el post debe de retornar 201 y cuando algo sale mal (Tidi esti se agrega en el controlador)
        }


    }
}
