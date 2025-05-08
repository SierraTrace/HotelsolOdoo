using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class TipoTemporadaController : ControllerBase
    {
        private readonly HotelContext _context;

        public TipoTemporadaController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTemporada>>> GetTipos()
        {
            return await _context.TipoTemporadas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TipoTemporada>> PostTipo(TipoTemporada tipo)
        {
            _context.TipoTemporadas.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipos), new { id = tipo.Id }, tipo);
        }
    }
}