using HotelSOL.Modelo;
using Xunit;

namespace HotelSOL.Test.ReservaHabitacionTest
{
    public class ReservaHabitacionRepositoryTest
    {
        [Fact]
        public void CalcularTotal_RetornaPrecioCorrecto()
        {
            var rh = new ReservaHabitacion
            {
                PrecioPorNoche = 150,
                Noches = 2
            };

            double total = rh.CalcularTotal();
            Assert.Equal(300, total);
        }
    }
}
