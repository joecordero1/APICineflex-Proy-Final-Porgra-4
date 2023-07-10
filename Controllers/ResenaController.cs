using APICineflex.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APICineflex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResenaController : ControllerBase
    {
        private readonly UsuariosCineflexContext _dbContext;

        //Constructor para contactar con la BDD
        public ResenaController(UsuariosCineflexContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("ListarResenas")]
        public async Task<IActionResult> listarResenas()
        {

            List<Resena> resenas = await _dbContext.Resenas.ToListAsync();


            return Ok(resenas);

        }

        [HttpPost]
        [Route("AnadirResena")]
        public async Task<IActionResult> guardarUsuario([FromQuery] string Titulo,
            [FromQuery] string Descripcion, [FromQuery] int idPeliculaP, [FromQuery] int idUserP)
        {
            var resena = new Resena();

            resena.Titulo = Titulo;
            resena.Descripcion = Descripcion;
            resena.IdPeliculaP = idPeliculaP;
            resena.IdUserP = idUserP;

            _dbContext.Resenas.Add(resena);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Resena ingresada con éxito",
                result = resena
            });
        }

        [HttpGet]
        [Route("BuscarResena")]
        public async Task<IActionResult> ObtenerResena(int id)
        {
            var resena = await _dbContext.Peliculas.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }

            return Ok(resena);
        }

        [HttpPut]
        [Route("EditarResena")]
        public async Task<IActionResult> guardarResena(int id, [FromQuery] string Titulo,
            [FromQuery] string Descripcion, [FromQuery] int idPeliculaP, [FromQuery] int idUserP)
        {
            var resena = await _dbContext.Resenas.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del resena existente con los valores del resena actualizado
            resena.Titulo = Titulo;
            resena.Descripcion = Descripcion;
            resena.IdPeliculaP = idPeliculaP;
            resena.IdUserP = idUserP;

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Resena actualizada con éxito",
                result = resena
            });
        }

        [HttpDelete]
        [Route("EliminarResena")]
        public async Task<IActionResult> BorrarResena(int id)
        {
            var resena = await _dbContext.Resenas.FindAsync(id);

            if (resena == null)
            {
                return NotFound();
            }

            _dbContext.Resenas.Remove(resena);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Resena eliminada con éxito",
                result = resena
            });
        }
    }
}
