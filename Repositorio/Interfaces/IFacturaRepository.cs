// IFacturaRepository.cs
using HotelSOL.Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Repositories
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> GetAllAsync();
        Task<Factura> GetByIdAsync(string id);
        Task AddAsync(Factura factura);
        Task UpdateAsync(Factura factura);
        Task DeleteAsync(string id);
        Task<bool> CrearFacturaConServiciosAsync(Factura factura);
        Task<List<Factura>> ObtenerFacturasPorClienteAsync(int clienteId);
        Task<Factura> GenerarFacturaParaClienteAsync(int clienteId);
        Task<string> ObtenerDetalleFacturaAsync(string facturaId);
    }
}