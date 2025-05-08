// DetalleFacturaServicio.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class DetalleFacturaServicio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ServicioId { get; set; }

        [ForeignKey("ServicioId")]
        public virtual Servicio Servicio { get; set; }

        [Required]
        public string FacturaId { get; set; }

        [ForeignKey("FacturaId")]
        public virtual Factura Factura { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public double PrecioUnitario { get; set; }

        public DetalleFacturaServicio() { }
    }
}
