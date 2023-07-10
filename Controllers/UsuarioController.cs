using APICineflex.Models.DB;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APICineflex.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuariosCineflexContext _dbContext;

        public UsuarioController(UsuariosCineflexContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<IActionResult> listarUsuario()
        {

            List<Usuario> usuarios = await _dbContext.Usuarios.ToListAsync();


            return Ok(usuarios);

        }

        [HttpPost]
        [Route("RegistrarUsuario")]
        public async Task<IActionResult> guardarUsuario([FromQuery] string Nombre, [FromQuery] int Rol,
            [FromQuery] string NombreUser, [FromQuery] string Contrasena,
            [FromQuery] string Correo)
        {
            var usuario = new Usuario();

            usuario.Nombre = Nombre;
            usuario.Rol = Rol;
            usuario.NombreUsuario = NombreUser;
            usuario.Contrasenia = Contrasena;
            usuario.Correo = Correo;

            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario registrado",
                result = usuario
            });
        }

        [HttpGet]
        [Route("BuscarUsuario")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<IActionResult> EditarUsuario(int id, [FromQuery] string Nombre, [FromQuery] int Rol,
            [FromQuery] string NombreUser, [FromQuery] string Contrasena,
            [FromQuery] string Correo)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades del usuario existente con los valores del usuario actualizado
            usuario.Nombre = Nombre;
            usuario.Rol = Rol;
            usuario.NombreUsuario = NombreUser;
            usuario.Contrasenia = Contrasena;
            usuario.Correo = Correo;

            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = "Usuario actualizado",
                result = usuario
            });
        }

        [HttpDelete]
        [Route("EliminarUsuario")]
        public async Task<IActionResult> BorrarUsuario(int id)
        {
            var usuario = await _dbContext.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            _dbContext.Usuarios.Remove(usuario);
            await _dbContext.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                message = "Usuario actualizado",
                result = usuario
            });
        }
    }

}
