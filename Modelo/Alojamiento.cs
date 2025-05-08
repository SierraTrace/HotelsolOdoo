using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class Alojamiento
    {
        [Key]
        public string Id { get; set; }

        [ForeignKey("TipoAlojamiento")]
        public int TipoId { get; set; }
        public virtual TipoAlojamiento Tipo { get; set; }

        // Mapea a la columna real "Precio" en la tabla
        [Column("Precio")]
        public double PrecioBase { get; set; }

        [NotMapped]
        public string Nombre => $"{Id} - {Tipo?.Nombre}";

        // Navegación: reservas asociadas
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public Alojamiento() { }

        public Alojamiento(string id, int tipoId, TipoAlojamiento tipo, double precioBase)
        {
            Id = id;
            TipoId = tipoId;
            Tipo = tipo;
            PrecioBase = precioBase;
        }

        /// <summary>
        /// Calcula el precio total del alojamiento para una cantidad de días,
        /// aplicando el factor de temporada.
        /// </summary>
        public virtual double CalcularPrecioTotal(int dias, double factorTemporada)
            => dias * PrecioBase * factorTemporada;
    }
}
