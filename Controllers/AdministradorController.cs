using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorRepository _repositorio;

        public AdministradorController(IAdministradorRepository repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> ObtenerUsuarios()
        {
            var usuarios = await _repositorio.ObtenerTodosLosUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost("usuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            var creado = await _repositorio.CrearUsuarioAsync(usuario);
            if (creado)
                return Ok("Usuario creado correctamente.");
            return BadRequest("Error al crear el usuario.");
        }

        [HttpPut("usuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            var actualizado = await _repositorio.ActualizarUsuarioAsync(usuario);
            if (actualizado)
                return Ok("Usuario actualizado correctamente.");
            return NotFound("Usuario no encontrado.");
        }

        [HttpDelete("usuario/{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var eliminado = await _repositorio.EliminarUsuarioAsync(id);
            if (eliminado)
                return Ok("Usuario eliminado correctamente.");
            return NotFound("Usuario no encontrado.");
        }

        [HttpPost("configuracion")]
        public async Task<IActionResult> ConfigurarSistema([FromBody] Dictionary<string, string> configuraciones)
        {
            var resultado = await _repositorio.GuardarConfiguracionSistemaAsync(configuraciones);
            if (resultado)
                return Ok("Configuración guardada correctamente.");
            return BadRequest("Error al guardar la configuración.");
        }
    }
}
