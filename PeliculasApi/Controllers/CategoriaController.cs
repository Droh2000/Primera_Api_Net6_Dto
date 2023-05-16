using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Data;
using PeliculasApi.Models;

namespace PeliculasApi.Controllers
{
    // Hay que fijarse que al crear el controlador
    // Dar click en API y no MVC (Que biene por defecto) y que sea en Blanco
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        //Creamos la referencia a la clase que tiene el DbContext
        private readonly AplicationDbContext _db;

        public CategoriaController(AplicationDbContext db)
        {
            _db = db; // con esto ya podemos usar las sentencias de DbContext
        }

        // Creacion de Metodos

        //Este nos retorna todo el contenido de la tabla Categoria
        [HttpGet]// de tipo GET 
        // A continuacion le vamos a indicar el TIPO DE CODIGO HTTP que debe lanzar cada metodo
        [ProducesResponseType(200, Type = typeof(List<Categoria>))]// Aqui sera el codigo 200 y nos sale esto nos retornara una lista
        // Cuando algo no funcione
        [ProducesResponseType(400)]// Bad Request
        public async Task<IActionResult> GetCategorias() {
            // El await se pone siempre al ser un metodo asincrono
            // Aqui vamos a retornar una lista de todas las categorias
            var lista = await _db.Categorias.OrderBy(c => c.Nombre).ToListAsync();

            return Ok(lista);
            /*
             Al Ejecutar en el Swagger vemos que ya se creo la seccion de Categoria
             Con el metodo Get y la ruta asignada
            Para probar se le pueden ingresar datos en la Base de datos
            Al Executer abajo tenemos en Fromato JSON el contenido de la Tabla Categoria
             */
        }

        // Metodo que nos retorne un registro requerido por su ID
        // como tenemos Dos Get Para diferenciar los Metodos Get
        // Se le pone el parametro entre [] (Con el Paremtro Name = es la forma en la que podemos invocar entre si los metodos)
        [HttpGet("{id:int}", Name = "GetCategoria")]// este id con el mismo nombre del paremtro del metodo
        [ProducesResponseType(200, Type = typeof(Categoria))]// Aqui No retorna una Lista solo un Objeto de tipo categoria
        [ProducesResponseType(400)]
        // Tambien puede ser que no encuentre Nada por El Id
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCategoria(int id)
        {
            // con el metodo FirstOrDefaultAsync traemos un solo registro
            var obj = await _db.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            // En caso que el Id no exista
            if (obj == null) {
                return NotFound();
            }

            return Ok(obj);

        }// Con este en Swagger ya nos sale en categoria eeste metodo con el Id

        // Ahora querems Guardar Registros
        // Este metodo recibe todos los datos que le eviemos
        // el [FromBody] recibe los datos
        [HttpPost]// En Post NO SE CONSIDERAN El 200 pero si el 201 (Ya que es un Insert)
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]// Error del servidor
        public async Task<IActionResult> CrearCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null) {
                return BadRequest(ModelState);// Este Model state nos regresa todos los erroes encontrados
            }
            if (!ModelState.IsValid) {//Si No es valido 
                return BadRequest(ModelState);
            }
            await _db.AddAsync(categoria);
            await _db.SaveChangesAsync();
            // Al Ponerle un 201 arriba ya no se puede retornar un OK() por que este retorna un 200
            //return Ok();
            // Ya creado el registro llamaos al metodo GetCategoria y a este le tenemos que encviar el Id
            // y el id es iguala nuevo objeto creado Pero tambien tenemos que modificar el metodo GetCategoria
            // Ya que la forma en la que se invocan es por la propiedad Get o Posst entre [] con el paremetro Name =
            return CreatedAtRoute("GetCategoria", new { id = categoria.Id }, categoria);
            // Esto al agregar una nueva categoria y llama al metodo para obtener el nuevo Registro

        }// En swagger ya sale el POST
        // En la caja de texto le editamos y le agregamos el Id se puede borrar al ser automatico
        // en executar verificar que salga codigo 200 para que este OK

    }
}
