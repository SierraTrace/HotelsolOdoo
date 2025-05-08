using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public class AlojamientoRepository : IAlojamientoRepository
    {
        private readonly HotelContext _context;
        public AlojamientoRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Alojamiento>> GetAllAsync()
        {
            return await _context.Alojamientos
                .Include(a => a.Tipo)      
                .Include(a => a.Reservas)  
                .ToListAsync();
        }

        public async Task<Alojamiento> GetByIdAsync(string id)
        {
            return await _context.Alojamientos
                .Include(a => a.Tipo)
                .Include(a => a.Reservas)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Alojamiento alojamiento)
        {
            _context.Alojamientos.Add(alojamiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Alojamiento alojamiento)
        {
            _context.Entry(alojamiento).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var alojamiento = await GetByIdAsync(id);
            if (alojamiento != null)
            {
                _context.Alojamientos.Remove(alojamiento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
