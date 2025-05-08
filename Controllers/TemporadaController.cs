using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemporadaController : ControllerBase
    {
        private readonly HotelContext _context;

        public TemporadaController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temporada>>> GetTemporadas()
        {
            return await _context.Temporadas
                .Include(t => t.TipoTemporada)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Temporada>> PostTemporada(Temporada temporada)
        {
            _context.Temporadas.Add(temporada);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTemporadas), new { id = temporada.Id }, temporada);
        }
    }
}