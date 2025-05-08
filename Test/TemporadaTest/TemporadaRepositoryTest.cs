using HotelSOL.Modelo;
using Xunit;

namespace HotelSOL.Tests.TemporadaTest
{
    public class TemporadaRepositoryTest
    {
        [Fact]
        public void CalcularFactorPrecio_DevuelveFactor()
        {
            var temporada = new Temporada { FactorPrecio = 1.1 };
            double factor = temporada.CalcularFactorPrecio();
            Assert.Equal(1.1, factor);
        }
    }
}
