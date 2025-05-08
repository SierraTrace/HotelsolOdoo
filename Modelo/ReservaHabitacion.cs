using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class ReservaHabitacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Reserva")]
        public int ReservaId { get; set; }
        public virtual Reserva Reserva { get; set; }

        [ForeignKey("Habitacion")]
        public int HabitacionId { get; set; }
        public virtual Habitacion Habitacion { get; set; }

        public double PrecioPorNoche { get; set; }
        public int Noches { get; set; }

        // Constructor
        public ReservaHabitacion() { }

        public ReservaHabitacion(int reservaId, int habitacionId, double precioPorNoche, int noches)
        {
            ReservaId = reservaId;
            HabitacionId = habitacionId;
            PrecioPorNoche = precioPorNoche;
            Noches = noches;
        }

        // Método para calcular el total de la estancia de esta habitación
        public double CalcularTotal() => PrecioPorNoche * Noches;

        internal static bool any()
        {
            throw new NotImplementedException();
        }
    }
}