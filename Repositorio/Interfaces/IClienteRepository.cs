using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelSOL.Modelo;

namespace HotelSOL.Repositorio.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Reserva>> ConsultarReservasAsync(int clienteId);
        Task<List<Reserva>> ConsultarReservasConDetallesAsync(int clienteId);
        Task<List<Reserva>> ConsultarReservasActivasAsync(int clienteId);
        Task<bool> RealizarReservaConHabitacionAsync(Reserva reserva, int habitacionId);
        Task<Cliente?> AutenticarClienteAsync(string email, string password);
        Task<List<Habitacion>> ConsultarDisponibilidadAsync(DateTime entrada, DateTime salida);
        Task<bool> RealizarReservaAsync(Reserva reserva);
        Task<bool> ModificarReservaAsync(Reserva reserva);
        Task<bool> AnularReservaAsync(int reservaId);
        Task<List<HistoricoEstancia>> ObtenerHistorialEstanciasAsync(int clienteId);
        Task<List<Factura>> ConsultarFacturasAsync(int clienteId);
        Task<List<Factura>> ConsultarFacturasConDetallesAsync(int clienteId);
        Task<List<Factura>> ConsultarFacturasPendientesAsync(int clienteId);

        /// <summary>
        /// Realiza el pago de la factura identificada por su Id numérico.
        /// </summary>
        Task<bool> RealizarPagoAsync(int facturaId, string metodoPago);

        Task<bool> SolicitarServicioParaClienteAsync(int clienteId, string servicioId);
        Task<bool> SolicitarServicioParaReservaAsync(int reservaId, string servicioId);
        Task<List<Servicio>> ObtenerServiciosAsync();
        Task<List<TipoReserva>> ObtenerTiposReservaAsync();
    }
}
