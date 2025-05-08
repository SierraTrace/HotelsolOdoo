using System.ComponentModel.DataAnnotations;

namespace HotelSOL.Modelo
{
    public class TipoAlojamiento
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
