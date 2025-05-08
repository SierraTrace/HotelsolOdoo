using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class ReservaServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public virtual Reserva Reserva { get; set; }

        [ForeignKey("Servicio")]
        public string ServicioId { get; set; }
        public virtual Servicio Servicio { get; set; }

        public int Cantidad { get; set; } = 1;
        public double PrecioUnitario { get; set; }
    }
}