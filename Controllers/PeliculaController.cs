using APICineflex.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APICineflex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeliculaController : ControllerBase
    {
        private readonly UsuariosCineflexContext _dbContext;

        public PeliculaController(UsuariosCineflexContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("ListarPeliculas")]
        public async Task<IActionResult> listarUsuario()
        {

            List<Pelicula> peliculas = await _dbContext.Peliculas.ToListAsync();


            return Ok(peliculas);

        }

        [HttpPost]
        [Route("AnadirPelicula")]
        public async Task<IActionResult> guardarPelicula([FromQuery] string Nombre,
            [FromQuery] string Descripcion, [FromQuery] string Genero, [FromQuery] int Anio,
            [FromQuery] string Poster)
        {
            var pelicula = new Pelicula();

            pelicula.Nombre = Nombre;
            pelicula.Descripcion = Descripcion;
            pelicula.Genero = Genero;
            pelicula.Anio = Anio;
            pelicula.Poster = Poster;

            _dbContext.Peliculas.Add(pelicula);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Pelicula ingresada con éxito",
                result = pelicula
            });
        }

        [HttpGet]
        [Route("BuscarPelicula")]
        public async Task<IActionResult> ObtenerPelicula(int id)
        {
            var pelicula = await _dbContext.Peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            return Ok(pelicula);
        }

        [HttpPut]
        [Route("EditarPelicula")]
        public async Task<IActionResult> EditarPelicula(int id, [FromQuery] string Nombre,
            [FromQuery] string Descripcion, [FromQuery] string Genero, [FromQuery] int Anio,
            [FromQuery] string Poster)
        {
            var pelicula = await _dbContext.Peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del pelicula existente con los valores del pelicula actualizado
            pelicula.Nombre = Nombre;
            pelicula.Descripcion = Descripcion;
            pelicula.Genero = Genero;
            pelicula.Anio = Anio;
            pelicula.Poster = Poster;

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Pelicula actualizada con éxito",
                result = pelicula
            });
        }

        [HttpDelete]
        [Route("EliminarPelicula")]
        public async Task<IActionResult> BorraPelicula(int id)
        {
            var pelicula = await _dbContext.Peliculas.FindAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            _dbContext.Peliculas.Remove(pelicula);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Pelicula eliminada con éxito",
                result = pelicula
            });
        }
    }
}

