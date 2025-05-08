using System.ComponentModel.DataAnnotations;

namespace HotelSOL.Modelo
{
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
