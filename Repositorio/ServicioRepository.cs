using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly HotelContext _context;
        public ServicioRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Servicio>> GetAllAsync()
        {
            return await _context.Servicios.ToListAsync();
        }

        public async Task<Servicio> GetByIdAsync(string id)
        {
            return await _context.Servicios.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Servicio servicio)
        {
            _context.Entry(servicio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var servicio = await GetByIdAsync(id);
            if (servicio != null)
            {
                _context.Servicios.Remove(servicio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
