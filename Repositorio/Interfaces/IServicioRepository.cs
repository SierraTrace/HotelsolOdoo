using HotelSOL.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public interface IServicioRepository
    {
        Task<IEnumerable<Servicio>> GetAllAsync();
        Task<Servicio> GetByIdAsync(string id);
        Task AddAsync(Servicio servicio);
        Task UpdateAsync(Servicio servicio);
        Task DeleteAsync(string id);
    }
}
