using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class Temporada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("TipoTemporada")]
        public int TipoTemporadaId { get; set; }
        public virtual TipoTemporada TipoTemporada { get; set; }

        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public double FactorPrecio { get; set; }

        public Temporada() { }

        public Temporada(TipoTemporada tipoTemporada, DateTime fechaInicio, DateTime fechaFin, double factorPrecio)
        {
            TipoTemporada = tipoTemporada;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            FactorPrecio = factorPrecio;
        }

        public virtual double CalcularFactorPrecio()
        {
            return FactorPrecio;
        }
    }
}
