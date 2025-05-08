using HotelSOL.Modelo;

namespace HotelSOL.Repositorios.Interfaces
{
    public interface IReservaHabitacionRepository
    {
        Task<List<ReservaHabitacion>> ObtenerTodasAsync();
        Task CrearAsync(ReservaHabitacion reservaHabitacion);
    }
}