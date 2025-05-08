// Factura.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class Factura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public int ReservaId { get; set; }

        public DateTime FechaEmision { get; set; } = DateTime.Now;
        public double Total { get; set; }
        public string MetodoPago { get; set; } = "Pendiente";
        public bool Pagada { get; set; } = false;

        [ForeignKey("ReservaId")]
        public virtual Reserva Reserva { get; set; }

        // ¡Aquí está el cambio! La propiedad se llama 'Detalles'
        public virtual ICollection<DetalleFacturaServicio> Detalles { get; set; }
            = new List<DetalleFacturaServicio>();

        public Factura()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Factura(int reservaId, double total, string metodoPago = "Pendiente")
        {
            Id = Guid.NewGuid().ToString();
            ReservaId = reservaId;
            Total = total;
            MetodoPago = metodoPago;
            FechaEmision = DateTime.Now;
            Pagada = false;
        }
    }
}
