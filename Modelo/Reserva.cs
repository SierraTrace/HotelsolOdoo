// Reserva.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [Required]
        public int TipoReservaId { get; set; }
        public virtual TipoReserva TipoReserva { get; set; }

        [Required]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        [Required]
        public DateTime FechaEntrada { get; set; }

        [Required]
        public DateTime FechaSalida { get; set; }

        [Required]
        public string AlojamientoId { get; set; }
        public virtual Alojamiento Alojamiento { get; set; }

        [Required]
        public int CalendarioId { get; set; }
        public virtual Calendario Calendario { get; set; }

        public virtual ICollection<ReservaHabitacion> ReservaHabitaciones { get; set; }
            = new List<ReservaHabitacion>();

        public virtual ICollection<ReservaServicio> Servicios { get; set; }
            = new List<ReservaServicio>();

        public virtual Factura Factura { get; set; }

        /// <summary>
        /// Inicializa la reserva. Por el momento solo valida que la fecha de salida sea posterior.
        /// </summary>
        public void CrearReserva()
        {
            if (FechaSalida <= FechaEntrada)
                throw new InvalidOperationException("La fecha de salida debe ser posterior a la fecha de entrada.");
            // aquí podrías añadir más validaciones (habitaciones, cliente, etc.)
        }

        /// <summary>
        /// Marca esta reserva como cancelada, cambiando su estado a "Cancelada".
        /// </summary>
        public void CancelarReserva()
        {
            if (Estado == null)
                Estado = new Estado();
            Estado.Nombre = "Cancelada";
        }

        /// <summary>
        /// Marca esta reserva como confirmada, cambiando su estado a "Confirmada".
        /// </summary>
        public void ConfirmarReserva()
        {
            if (Estado == null)
                Estado = new Estado();
            Estado.Nombre = "Confirmada";
        }
    }
}
