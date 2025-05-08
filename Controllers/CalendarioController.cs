using HotelSOL.Modelo;
using HotelSOL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarioController : ControllerBase
    {
        private readonly ICalendarioRepository _calendarioRepository;
        public CalendarioController(ICalendarioRepository calendarioRepository)
        {
            _calendarioRepository = calendarioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calendario>>> GetCalendarios()
        {
            var calendarios = await _calendarioRepository.GetAllAsync();
            return Ok(calendarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calendario>> GetCalendario(int id)
        {
            var calendario = await _calendarioRepository.GetByIdAsync(id);
            if (calendario == null)
                return NotFound();
            return Ok(calendario);
        }

        [HttpPost]
        public async Task<ActionResult<Calendario>> PostCalendario(Calendario calendario)
        {
            await _calendarioRepository.AddAsync(calendario);
            return CreatedAtAction(nameof(GetCalendario), new { id = calendario.Id }, calendario);
        }
    }
}
