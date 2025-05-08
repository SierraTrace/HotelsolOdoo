using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public class DetalleFacturaServicioRepository : IDetalleFacturaServicioRepository
    {
        private readonly HotelContext _context;
        public DetalleFacturaServicioRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DetalleFacturaServicio>> GetAllAsync()
        {
            return await _context.DetalleFacturaServicios
                .Include(d => d.Servicio)
                .Include(d => d.Factura)
                .ToListAsync();
        }

        public async Task<DetalleFacturaServicio> GetByIdAsync(int id)
        {
            return await _context.DetalleFacturaServicios
                .Include(d => d.Servicio)
                .Include(d => d.Factura)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(DetalleFacturaServicio detalle)
        {
            _context.DetalleFacturaServicios.Add(detalle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DetalleFacturaServicio detalle)
        {
            _context.Entry(detalle).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var detalle = await GetByIdAsync(id);
            if (detalle != null)
            {
                _context.DetalleFacturaServicios.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
