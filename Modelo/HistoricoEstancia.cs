using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class HistoricoEstancia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("Habitacion")]
        public int HabitacionId { get; set; }
        public virtual Habitacion Habitacion { get; set; }

        [ForeignKey("Factura")]
        public string FacturaId { get; set; }
        public virtual Factura Factura { get; set; }

        public DateTime FechaEntrada { get; set; }
        public DateTime FechaSalida { get; set; }

        public HistoricoEstancia() { }

        public HistoricoEstancia(Cliente cliente, Habitacion habitacion, DateTime fechaEntrada, DateTime fechaSalida, Factura factura)
        {
            Cliente = cliente;
            Habitacion = habitacion;
            FechaEntrada = fechaEntrada;
            FechaSalida = fechaSalida;
            Factura = factura;
        }

        public virtual void RegistrarEstancia()
        {
            if (FechaEntrada >= FechaSalida)
                throw new InvalidOperationException("La fecha de entrada debe ser anterior a la fecha de salida.");
        }

        public virtual List<HistoricoEstancia> ConsultarEstanciaPorCliente(Cliente cliente)
        {
            return new List<HistoricoEstancia>();
        }
    }
}
