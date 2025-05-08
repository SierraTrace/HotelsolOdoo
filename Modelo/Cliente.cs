using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace HotelSOL.Modelo
{
    //[Table("Clientes")]
    public class Cliente : Usuario
    {
        public bool EsVIP { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Id} - {Nombre} {Apellido}";


        public Cliente() { }

        public Cliente(string nombre, string apellido, string email, string password, int movil, bool esVIP)
            : base(nombre, apellido, email, password, movil)
        {
            EsVIP = esVIP;
        }

        // Devuelve habitaciones disponibles para las fechas seleccionadas
        public virtual List<Habitacion> ConsultarDisponibilidad(DateTime fechaEntrada, DateTime fechaSalida) => new();

        // Realiza una reserva
        public virtual void RealizarReserva(Reserva reserva) { }

        // Modifica una reserva existente
        public virtual void ModificarReserva(Reserva reserva) { }

        // Anula una reserva
        public virtual void AnularReserva(string reservaId) { }

        // Muestra el historial de estancias del cliente
        public virtual List<HistoricoEstancia> ConsultarHistorialEstancias() => new();

        // Solicita un servicio
        public virtual void SolicitarServicio(Servicio servicio) { }

        // Consulta facturas del cliente
        public virtual List<Factura> ConsultarFactura() => new();

        // Registra el pago de una factura
        public virtual bool RealizarPago(Factura factura, string metodoPago) => true;
    }
}
