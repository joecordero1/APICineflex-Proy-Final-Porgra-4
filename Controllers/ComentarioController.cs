using APICineflex.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace APICineflex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly UsuariosCineflexContext _dbContext;

        public ComentarioController(UsuariosCineflexContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("ListarComentarios")]
        public async Task<IActionResult> listarComentarios()
        {

            List<Comentario> comentarios = await _dbContext.Comentarios.ToListAsync();


            return Ok(comentarios);

        }

        [HttpPost]
        [Route("AgregarComentario")]
        public async Task<IActionResult> guardarComentario([FromQuery] int idUsuario, [FromQuery] int idResena,
            [FromQuery] string Cuerpo, [FromQuery] DateTime FechaComentario)
        {
            var comentario = new Comentario();

            comentario.IdUserF = idUsuario;
            comentario.IdResenaF = idResena;
            comentario.Cuerpo = Cuerpo;
            comentario.FechaComentario = FechaComentario;

            _dbContext.Comentarios.Add(comentario);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Comentario ingresado con éxito",
                result = comentario
            });
        }

        [HttpGet]
        [Route("BuscarComentario")]
        public async Task<IActionResult> ObtenerComentario(int id)
        {
            var comentario = await _dbContext.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }

        [HttpPut]
        [Route("EditarPelicula")]
        public async Task<IActionResult> EditarComentario(int id, [FromQuery] int idUsuario, [FromQuery] int idResena,
            [FromQuery] string Cuerpo, [FromQuery] DateTime FechaComentario)
        {
            var comentario = await _dbContext.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del pelicula existente con los valores del pelicula actualizado
            comentario.IdUserF = idUsuario;
            comentario.Cuerpo = Cuerpo;
            comentario.FechaComentario = FechaComentario;

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Comentario actualizado con éxito",
                result = comentario
            });
        }

        [HttpDelete]
        [Route("EliminarPelicula")]
        public async Task<IActionResult> BorrarComentario(int id)
        {
            var comentario = await _dbContext.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            _dbContext.Comentarios.Remove(comentario);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Comentario eliminado con éxito",
                result = comentario
            });
        }
    }
}
