using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorios
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly HotelContext _context;

        public ReservaRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> ObtenerTodasAsync()
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

        public async Task<Reserva?> ObtenerPorIdAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.ReservaHabitaciones)
                    .ThenInclude(rh => rh.Habitacion)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CrearAsync(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task CancelarAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) throw new Exception("Reserva no encontrada");

            reserva.CancelarReserva();
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmarAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) throw new Exception("Reserva no encontrada");

            reserva.ConfirmarReserva();
            await _context.SaveChangesAsync();
        }
    }
}
