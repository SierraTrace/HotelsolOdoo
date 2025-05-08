// IRecepcionistaRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSOL.Modelo;

namespace HotelSOL.Repositorio.Interfaces
{
    public interface IRecepcionistaRepository
    {
        Task<Recepcionista?> AutenticarRecepcionistaAsync(string email, string password);
        Task<bool> RegistrarClienteAsync(Cliente cliente);
        Task<bool> ActualizarClienteAsync(Cliente cliente);
        Task<bool> RealizarReservaAsync(Reserva reserva);
        Task<bool> ModificarReservaAsync(Reserva reserva);
        Task<bool> AnularReservaAsync(int reservaId);
        Task<List<Habitacion>> ConsultarDisponibilidadAsync(DateTime entrada, DateTime salida);
        Task<List<object>> ConsultarEntradasAsync(DateTime fecha);
        Task<List<Reserva>> ConsultarSalidasAsync(DateTime fecha);
        Task<bool> FirmarEntradaAsync(int reservaId);
        Task<List<Cliente>> ObtenerClientesAsync();
        Task<List<HistoricoEstancia>> ObtenerHistoricoClienteAsync(int clienteId);
        Task<bool> CrearFacturaAsync(Factura factura);
        Task<List<Factura>> ConsultarFacturasClienteAsync(int clienteId);
        Task<bool> RegistrarPagoAsync(string facturaId, string metodoPago);
        Task<bool> SolicitarServicioParaClienteAsync(int clienteId, string servicioId);
        Task<Reserva?> ObtenerReservaActualDelClienteAsync(int clienteId);
        Task<List<Servicio>> ObtenerServiciosAsync();
        Task<List<TipoReserva>> ObtenerTiposReservaAsync();
        Task<List<AlojamientoComboDto>> ObtenerAlojamientosAsync();
        Task<List<Estado>> ObtenerEstadosReservaAsync();
    }
}
