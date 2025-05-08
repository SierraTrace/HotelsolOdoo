using HotelSOL.Modelo;

namespace HotelSOL.Repositorios.Interfaces
{
    public interface ITemporadaRepository
    {
        Task<List<Temporada>> ObtenerTodasAsync();
        Task<Temporada?> ObtenerTemporadaActualAsync(DateTime fecha);
        Task CrearAsync(Temporada temporada);
    }
}