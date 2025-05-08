using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoEstanciaController : ControllerBase
    {
        private readonly HotelContext _context;

        public HistoricoEstanciaController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoricoEstancia>>> GetHistorico()
        {
            return await _context.HistoricoEstancias
                .Include(h => h.Cliente)
                .Include(h => h.Habitacion)
                .Include(h => h.Factura)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<HistoricoEstancia>> PostHistorico(HistoricoEstancia entrada)
        {
            _context.HistoricoEstancias.Add(entrada);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHistorico), new { id = entrada.Id }, entrada);
        }
    }
}