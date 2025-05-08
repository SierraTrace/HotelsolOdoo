using System;
using System.ComponentModel.DataAnnotations;

namespace HotelSOL.Modelo
{
    public class Servicio
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "El nombre del servicio es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El precio del servicio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un valor no negativo.")]
        public double Precio { get; set; }

        // Añadido campo Descripcion
        public string Descripcion { get; set; }

        public Servicio()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Servicio(string nombre, double precio, string descripcion = "")
        {
            Id = Guid.NewGuid().ToString();
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
        }
    }
}