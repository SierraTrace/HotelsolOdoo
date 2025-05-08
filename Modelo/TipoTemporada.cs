using System.ComponentModel.DataAnnotations;

namespace HotelSOL.Modelo
{
    public class TipoTemporada
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
