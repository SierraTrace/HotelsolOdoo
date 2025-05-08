using HotelSOL.Modelo;

namespace HotelSOL.Repositorios.Interfaces
{
    public interface IReservaRepository
    {
        Task<List<Reserva>> ObtenerTodasAsync();
        Task<Reserva?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Reserva reserva);
        Task CancelarAsync(int id);
        Task ConfirmarAsync(int id);
    }
}