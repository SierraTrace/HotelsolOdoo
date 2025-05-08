using HotelSOL.Modelo;
using HotelSOL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlojamientoController : ControllerBase
    {
        private readonly IAlojamientoRepository _alojamientoRepository;

        public AlojamientoController(IAlojamientoRepository alojamientoRepository)
        {
            _alojamientoRepository = alojamientoRepository;
        }

        // GET: api/Alojamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alojamiento>>> GetAlojamientos()
        {
            var alojamientos = await _alojamientoRepository.GetAllAsync();
            return Ok(alojamientos);
        }

        // GET: api/Alojamiento/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Alojamiento>> GetAlojamiento(string id)
        {
            var alojamiento = await _alojamientoRepository.GetByIdAsync(id);
            if (alojamiento == null)
                return NotFound("Alojamiento no encontrado.");

            return Ok(alojamiento);
        }

        // POST: api/Alojamiento
        [HttpPost]
        public async Task<ActionResult> PostAlojamiento([FromBody] Alojamiento alojamiento)
        {
            if (alojamiento == null)
                return BadRequest("Datos inválidos.");

            alojamiento.Id = Guid.NewGuid().ToString();
            await _alojamientoRepository.AddAsync(alojamiento);

            return CreatedAtAction(nameof(GetAlojamiento), new { id = alojamiento.Id }, alojamiento);
        }

        // PUT: api/Alojamiento/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAlojamiento(string id, [FromBody] Alojamiento alojamiento)
        {
            if (id != alojamiento.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            var existente = await _alojamientoRepository.GetByIdAsync(id);
            if (existente == null)
                return NotFound("Alojamiento no encontrado.");

            await _alojamientoRepository.UpdateAsync(alojamiento);
            return NoContent();
        }

        // DELETE: api/Alojamiento/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAlojamiento(string id)
        {
            var alojamiento = await _alojamientoRepository.GetByIdAsync(id);
            if (alojamiento == null)
                return NotFound("Alojamiento no encontrado.");

            await _alojamientoRepository.DeleteAsync(id);
            return Ok("Alojamiento eliminado correctamente.");
        }
    }
}
