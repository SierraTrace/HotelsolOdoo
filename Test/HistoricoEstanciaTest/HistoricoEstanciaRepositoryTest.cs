using HotelSOL.Modelo;
using Xunit;

namespace HotelSOL.Tests.HistoricoEstanciaTest
{
    public class HistoricoEstanciaRepositoryTest
    {
        [Fact]
        public void RegistrarEstancia_ConFechasValidas_NoLanzaError()
        {
            var estancia = new HistoricoEstancia
            {
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2)
            };

            var ex = Record.Exception(() => estancia.RegistrarEstancia());
            Assert.Null(ex);
        }

        [Fact]
        public void RegistrarEstancia_ConFechasInvalidas_LanzaExcepcion()
        {
            var estancia = new HistoricoEstancia
            {
                FechaEntrada = DateTime.Today.AddDays(5),
                FechaSalida = DateTime.Today
            };

            Assert.Throws<InvalidOperationException>(() => estancia.RegistrarEstancia());
        }

        [Fact]
        public void ConsultarEstanciaPorCliente_SiempreDevuelveListaVacia()
        {
            var cliente = new Cliente { Id = 1, Nombre = "Pedro" };
            var estancia = new HistoricoEstancia();

            var lista = estancia.ConsultarEstanciaPorCliente(cliente);
            Assert.Empty(lista);
        }
    }
}
