using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public class CalendarioRepository : ICalendarioRepository
    {
        private readonly HotelContext _context;
        public CalendarioRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Calendario>> GetAllAsync()
        {
            return await _context.Calendarios
                .Include(c => c.Eventos)
                .Include(c => c.Alojamientos)
                .ToListAsync();
        }

        public async Task<Calendario> GetByIdAsync(int id)
        {
            return await _context.Calendarios
                .Include(c => c.Eventos)
                .Include(c => c.Alojamientos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Calendario calendario)
        {
            _context.Calendarios.Add(calendario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Calendario calendario)
        {
            _context.Entry(calendario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var calendario = await GetByIdAsync(id);
            if (calendario != null)
            {
                _context.Calendarios.Remove(calendario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
