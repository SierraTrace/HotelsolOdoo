using HotelSOL.Modelo;
using Xunit;

namespace HotelSOL.Tests.HabitacionTest
{
    public class HabitacionRepositoryTest
    {
        [Fact]
        public void ConsultarDisponibilidad_SinReservasYDisponible_DevuelveTrue()
        {
            var habitacion = new Habitacion
            {
                Disponibilidad = true,
                ReservaHabitaciones = new List<ReservaHabitacion>()
            };

            bool disponible = habitacion.ConsultarDisponibilidad(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.True(disponible);
        }

        [Fact]
        public void CalcularPrecio_AltaYPremium_CalculaCorrectamente()
        {
            var habitacion = new Habitacion { PrecioBase = 100 };
            var temporada = new Temporada { FactorPrecio = 1.25 };
            var tipoAlojamiento = new TipoAlojamiento { Nombre = "Premium" };

            double precio = habitacion.CalcularPrecio(tipoAlojamiento, temporada);
            Assert.Equal(100 * 1.25 * 1.5, precio);
        }
    }
}
