using HotelSOL.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public interface IDetalleFacturaServicioRepository
    {
        Task<IEnumerable<DetalleFacturaServicio>> GetAllAsync();
        Task<DetalleFacturaServicio> GetByIdAsync(int id);
        Task AddAsync(DetalleFacturaServicio detalle);
        Task UpdateAsync(DetalleFacturaServicio detalle);
        Task DeleteAsync(int id);
    }
}
