using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace HotelSOL.Modelo
{
    public class Calendario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Lista de reservas registradas en el calendario (por ejemplo, entradas y salidas)
        public virtual List<Reserva> Eventos { get; set; } = new List<Reserva>();

        // Nueva propiedad: lista de alojamientos (por ejemplo, habitaciones) que se gestionan en este calendario.
        // Esta lista se utiliza para determinar cuáles están disponibles en una fecha dada.
        public virtual List<Alojamiento> Alojamientos { get; set; } = new List<Alojamiento>();

        public Calendario() { }

        /// <summary>
        /// Retorna las reservas cuya fecha de entrada coincide exactamente con la fecha indicada.
        /// </summary>
        public virtual List<Reserva> ConsultarEntradas(DateTime fecha) =>
            Eventos.FindAll(e => e.FechaEntrada.Date == fecha.Date);

        /// <summary>
        /// Retorna las reservas cuya fecha de salida coincide exactamente con la fecha indicada.
        /// </summary>
        public virtual List<Reserva> ConsultarSalidas(DateTime fecha) =>
            Eventos.FindAll(e => e.FechaSalida.Date == fecha.Date);

        /// <summary>
        /// Consulta la disponibilidad de alojamientos para una fecha determinada.
        /// Se consideran ocupados aquellos alojamientos que tengan una reserva activa en la fecha:
        /// es decir, donde FechaEntrada <= fecha y FechaSalida > fecha.
        /// </summary>
        /// <param name="fecha">Fecha en la que se consulta la disponibilidad.</param>
        /// <returns>Lista de alojamientos disponibles en esa fecha.</returns>
        public virtual List<Alojamiento> ConsultarDisponibilidad(DateTime fecha)
        {
            // Se extraen los IDs de los alojamientos que tienen una reserva activa en la fecha indicada.
            var reservados = Eventos
                .Where(e => e.FechaEntrada.Date <= fecha.Date && e.FechaSalida.Date > fecha.Date)
                .Select(e => e.AlojamientoId)
                .Distinct();

            // Retorna aquellos alojamientos cuya Id no figura en la lista de ocupados.
            return Alojamientos.Where(a => !reservados.Contains(a.Id)).ToList();
        }
    }
}
