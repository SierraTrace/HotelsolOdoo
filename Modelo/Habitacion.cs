using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class Habitacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TipoHabitacion")]
        public int TipoHabitacionId { get; set; }
        public virtual TipoHabitacion TipoHabitacion { get; set; }

        public string Numero { get; set; }
        public int Capacidad { get; set; }

        // Mapea a la columna real "Precio" en la tabla
        [Column("Precio")]
        public double PrecioBase { get; set; }

        public bool Disponibilidad { get; set; }

        public virtual List<ReservaHabitacion> ReservaHabitaciones { get; set; } = new();

        public Habitacion() { }

        public Habitacion(TipoHabitacion tipoHabitacion, int capacidad, double precioBase, bool disponibilidad, List<ReservaHabitacion> reservaHabitaciones)
        {
            TipoHabitacion = tipoHabitacion;
            Capacidad = capacidad;
            PrecioBase = precioBase;
            Disponibilidad = disponibilidad;
            ReservaHabitaciones = reservaHabitaciones;
        }

        /// <summary>
        /// Comprueba si la habitación está libre entre dos fechas,
        /// basándose en las reservas existentes.
        /// </summary>
        public virtual bool ConsultarDisponibilidad(DateTime fechaInicio, DateTime fechaFin)
        {
            if (!Disponibilidad)
                return false;

            foreach (var rh in ReservaHabitaciones)
            {
                if (rh.Reserva != null)
                {
                    var entrada = rh.Reserva.FechaEntrada;
                    var salida = rh.Reserva.FechaSalida;
                    if (fechaInicio < salida && fechaFin > entrada)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Calcula un precio según tipo de alojamiento y temporada.
        /// </summary>
        public virtual double CalcularPrecio(TipoAlojamiento tipoAloj, Temporada temporada)
        {
            double factor = temporada?.FactorPrecio ?? 1.0;
            return PrecioBase * factor;
        }
    }
}
