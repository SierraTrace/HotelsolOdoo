using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly HotelContext _context;

        public ReservaController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.ReservaHabitaciones)
                    .ThenInclude(rh => rh.Habitacion)
                .Include(r => r.TipoReserva)
                .Include(r => r.Alojamiento)
                .Include(r => r.Estado)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            try
            {
                // Validaciones básicas
                if (reserva.FechaEntrada >= reserva.FechaSalida)
                    return BadRequest("La fecha de entrada debe ser anterior a la fecha de salida.");

                if (reserva.ReservaHabitaciones == null || !reserva.ReservaHabitaciones.Any())
                    return BadRequest("Debe asignar al menos una habitación a la reserva.");

                foreach (var reservaHab in reserva.ReservaHabitaciones)
                {
                    var habitacion = await _context.Habitaciones
                        .Include(h => h.ReservaHabitaciones)
                            .ThenInclude(rh => rh.Reserva)
                        .FirstOrDefaultAsync(h => h.Id == reservaHab.HabitacionId);

                    if (habitacion == null)
                        return BadRequest($"La habitación {reservaHab.HabitacionId} no existe.");

                    // Validar disponibilidad
                    bool ocupada = habitacion.ReservaHabitaciones.Any(rh =>
                        rh.Reserva.FechaEntrada < reserva.FechaSalida &&
                        rh.Reserva.FechaSalida > reserva.FechaEntrada);

                    if (ocupada)
                        return BadRequest($"La habitación {habitacion.Id} no está disponible en las fechas seleccionadas.");

                    int noches = (reserva.FechaSalida - reserva.FechaEntrada).Days;
                    reservaHab.Noches = noches;
                    reservaHab.PrecioPorNoche = habitacion.PrecioBase;
                }

                // Estado inicial: Pendiente
                var estadoPendiente = await _context.Estados.FirstOrDefaultAsync(e => e.Nombre == "Pendiente");
                if (estadoPendiente == null)
                    return BadRequest("No se encontró el estado 'Pendiente'.");

                reserva.EstadoId = estadoPendiente.Id;

                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetReservas), new { id = reserva.Id }, reserva);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la reserva: {ex.Message}");
            }
        }
    }
}