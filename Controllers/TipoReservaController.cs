using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoReservaController : ControllerBase
    {
        private readonly HotelContext _context;

        public TipoReservaController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoReserva>>> GetTipos()
        {
            return await _context.TipoReservas.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TipoReserva>> PostTipo(TipoReserva tipo)
        {
            _context.TipoReservas.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipos), new { id = tipo.Id }, tipo);
        }
    }
}
