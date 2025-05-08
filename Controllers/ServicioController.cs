using HotelSOL.Modelo;
using HotelSOL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioController : ControllerBase
    {
        private readonly IServicioRepository _servicioRepository;

        public ServicioController(IServicioRepository servicioRepository)
        {
            _servicioRepository = servicioRepository;
        }

        // GET: api/Servicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            var servicios = await _servicioRepository.GetAllAsync();
            return Ok(servicios);
        }

        // POST: api/Servicio
        [HttpPost]
        public async Task<ActionResult<Servicio>> PostServicio([FromBody] Servicio servicio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Si el Id no se asigna, generamos un nuevo GUID
            if (string.IsNullOrEmpty(servicio.Id))
            {
                servicio.Id = Guid.NewGuid().ToString();
            }

            await _servicioRepository.AddAsync(servicio);
            return CreatedAtAction(nameof(GetServicios), new { id = servicio.Id }, servicio);
        }
    }
}
