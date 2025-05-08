using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelSOL.Modelo
{
    public class TipoReserva
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public string Descripcion => $"{Id} - {Nombre}";
        public string Nombre { get; set; }
    }
}
