using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorios
{
    public class HabitacionRepository : IHabitacionRepository
    {
        private readonly HotelContext _context;

        public HabitacionRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Habitacion>> ObtenerTodasAsync()
        {
            return await _context.Habitaciones
                .Include(h => h.TipoHabitacion)
                .ToListAsync();
        }

        public async Task<Habitacion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Habitaciones
                .Include(h => h.ReservaHabitaciones)
                    .ThenInclude(rh => rh.Reserva)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Habitacion>> ObtenerDisponiblesAsync(DateTime inicio, DateTime fin)
        {
            var habitaciones = await _context.Habitaciones
                .Include(h => h.ReservaHabitaciones)
                    .ThenInclude(rh => rh.Reserva)
                .ToListAsync();

            return habitaciones
                .Where(h => h.ConsultarDisponibilidad(inicio, fin))
                .ToList();
        }

        public async Task CrearAsync(Habitacion habitacion)
        {
            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();
        }
    }
}

