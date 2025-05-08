using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorios
{
    public class TemporadaRepository : ITemporadaRepository
    {
        private readonly HotelContext _context;

        public TemporadaRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<List<Temporada>> ObtenerTodasAsync()
        {
            return await _context.Temporadas
                .Include(t => t.TipoTemporada)
                .ToListAsync();
        }

        public async Task<Temporada?> ObtenerTemporadaActualAsync(DateTime fecha)
        {
            return await _context.Temporadas
                .FirstOrDefaultAsync(t => fecha >= t.FechaInicio && fecha <= t.FechaFin);
        }

        public async Task CrearAsync(Temporada temporada)
        {
            _context.Temporadas.Add(temporada);
            await _context.SaveChangesAsync();
        }
    }
}
