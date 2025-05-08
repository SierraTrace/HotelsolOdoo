using HotelSOL.Modelo;

namespace HotelSOL.Repositorios.Interfaces
{
    public interface IHabitacionRepository
    {
        Task<List<Habitacion>> ObtenerTodasAsync();
        Task<Habitacion?> ObtenerPorIdAsync(int id);
        Task<List<Habitacion>> ObtenerDisponiblesAsync(DateTime inicio, DateTime fin);
        Task CrearAsync(Habitacion habitacion);
    }
}