using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HabitacionController : ControllerBase
    {
        private readonly HotelContext _context;

        public HabitacionController(HotelContext context)
        {
            _context = context;
        }

        // GET: api/habitacion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Habitacion>>> GetHabitaciones()
        {
            return await _context.Habitaciones
                .Include(h => h.TipoHabitacion)
                .ToListAsync();
        }

        // POST: api/habitacion
        [HttpPost]
        public async Task<ActionResult<Habitacion>> PostHabitacion(Habitacion habitacion)
        {
            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHabitaciones), new { id = habitacion.Id }, habitacion);
        }

        // GET: api/habitacion/disponibles?fechaInicio=2025-06-01&fechaFin=2025-06-05
        [HttpGet("disponibles")]
        public async Task<ActionResult<IEnumerable<Habitacion>>> GetHabitacionesDisponibles(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio >= fechaFin)
                return BadRequest("La fecha de inicio debe ser anterior a la fecha de fin.");

            var habitaciones = await _context.Habitaciones
                .Include(h => h.ReservaHabitaciones)
                    .ThenInclude(rh => rh.Reserva)
                .Include(h => h.TipoHabitacion)
                .ToListAsync();

            var disponibles = habitaciones
                .Where(h => h.ConsultarDisponibilidad(fechaInicio, fechaFin))
                .ToList();

            return Ok(disponibles);
        }
    }
}