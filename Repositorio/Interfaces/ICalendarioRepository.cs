using HotelSOL.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public interface ICalendarioRepository
    {
        Task<IEnumerable<Calendario>> GetAllAsync();
        Task<Calendario> GetByIdAsync(int id);
        Task AddAsync(Calendario calendario);
        Task UpdateAsync(Calendario calendario);
        Task DeleteAsync(int id);
    }
}
