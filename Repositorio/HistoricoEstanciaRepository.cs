using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorios
{
    public class HistoricoEstanciaRepository : IHistoricoEstanciaRepository
    {
        private readonly HotelContext _context;

        public HistoricoEstanciaRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<HistoricoEstancia>> ObtenerPorClienteAsync(int clienteId)
        {
            return await _context.HistoricoEstancias
                .Include(h => h.Habitacion)
                .Include(h => h.Factura)
                .Where(h => h.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task CrearAsync(HistoricoEstancia estancia)
        {
            _context.HistoricoEstancias.Add(estancia);
            await _context.SaveChangesAsync();
        }
    }
}
