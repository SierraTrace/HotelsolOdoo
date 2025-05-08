using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace HotelSOL.Modelo
{
    //[Table("Recepcionistas")]
    public class Recepcionista : Usuario
    {
        public Recepcionista() { }

        public Recepcionista(string nombre, string apellido, string email, string password, int movil)
            : base(nombre, apellido, email, password, movil) { }

        // Registra un nuevo cliente (requiere lógica en base de datos)
        public virtual void RegistrarCliente(Cliente cliente) { }

        // Asocia una reserva al sistema
        public virtual void RealizarReserva(Reserva reserva) { }

        // Modifica una reserva existente
        public virtual void ModificarReserva(Reserva reserva) { }

        // Anula una reserva por ID
        public virtual void AnularReserva(string reservaId) { }

        // Devuelve habitaciones disponibles entre dos fechas
        public virtual List<Habitacion> ConsultarDisponibilidad(DateTime fechaEntrada, DateTime fechaSalida) => new();

        // Consulta reservas de entrada para una fecha
        public virtual List<Reserva> ConsultarEntradas(DateTime fecha) => new();

        // Consulta reservas de salida para una fecha
        public virtual List<Reserva> ConsultarSalidas(DateTime fecha) => new();

        // Marca como firmada la entrada de una reserva
        public virtual void FirmarEntrada(Reserva reserva) { }

        // Devuelve todos los clientes registrados
        public virtual List<Cliente> ConsultarListadoClientes() => new();

        // Devuelve el historial de estancias de un cliente
        public virtual List<HistoricoEstancia> ConsultarHistoricoEstancias(Cliente cliente) => new();

        // Registra una factura en el sistema
        public virtual void RealizarFactura(Factura factura) { }

        // Devuelve facturas asociadas a un cliente
        public virtual List<Factura> ConsultarFactura(Cliente cliente) => new();

        // Registra un pago de factura
        public virtual bool RealizarPago(Factura factura, string metodoPago) => true;

        // Solicita un servicio adicional
        public virtual void SolicitarServicio(Servicio servicio) { }
    }
}
