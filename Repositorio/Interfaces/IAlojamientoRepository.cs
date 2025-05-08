using HotelSOL.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public interface IAlojamientoRepository
    {
        Task<IEnumerable<Alojamiento>> GetAllAsync();
        Task<Alojamiento> GetByIdAsync(string id);
        Task AddAsync(Alojamiento alojamiento);
        Task UpdateAsync(Alojamiento alojamiento);
        Task DeleteAsync(string id);
    }
}
