using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoAlojamientoController : ControllerBase
    {
        private readonly HotelContext _context;

        public TipoAlojamientoController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoAlojamiento>>> GetTipos()
        {
            return await _context.TipoAlojamientos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TipoAlojamiento>> PostTipo(TipoAlojamiento tipo)
        {
            _context.TipoAlojamientos.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipos), new { id = tipo.Id }, tipo);
        }
    }
    }