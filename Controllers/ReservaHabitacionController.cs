using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaHabitacionController : ControllerBase
    {
        private readonly HotelContext _context;

        public ReservaHabitacionController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaHabitacion>>> GetReservaHabitaciones()
        {
            return await _context.ReservaHabitaciones
                .Include(rh => rh.Reserva)
                .Include(rh => rh.Habitacion)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ReservaHabitacion>> PostReservaHabitacion(ReservaHabitacion reservaHabitacion)
        {
            _context.ReservaHabitaciones.Add(reservaHabitacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReservaHabitaciones), new { id = reservaHabitacion.Id }, reservaHabitacion);
        }
    }
}