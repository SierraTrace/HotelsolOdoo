using HotelSOL.Modelo;

namespace HotelSOL.Repositorios.Interfaces
{
    public interface IHistoricoEstanciaRepository
    {
        Task<List<HistoricoEstancia>> ObtenerPorClienteAsync(int clienteId);
        Task CrearAsync(HistoricoEstancia estancia);
    }
}