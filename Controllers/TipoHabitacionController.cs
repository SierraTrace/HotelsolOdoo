using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoHabitacionController : ControllerBase
    {
        private readonly HotelContext _context;

        public TipoHabitacionController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoHabitacion>>> GetTipos()
        {
            return await _context.TipoHabitaciones.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<TipoHabitacion>> PostTipo(TipoHabitacion tipo)
        {
            _context.TipoHabitaciones.Add(tipo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTipos), new { id = tipo.Id }, tipo);
        }
    }
    }