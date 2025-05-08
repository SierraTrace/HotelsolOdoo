using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorios
{
    public class ReservaHabitacionRepository : IReservaHabitacionRepository
    {
        private readonly HotelContext _context;

        public ReservaHabitacionRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<ReservaHabitacion>> ObtenerTodasAsync()
        {
            return await _context.ReservaHabitaciones
                .Include(rh => rh.Habitacion)
                .Include(rh => rh.Reserva)
                .ToListAsync();
        }

        public async Task CrearAsync(ReservaHabitacion reservaHabitacion)
        {
            _context.ReservaHabitaciones.Add(reservaHabitacion);
            await _context.SaveChangesAsync();
        }
    }
}
