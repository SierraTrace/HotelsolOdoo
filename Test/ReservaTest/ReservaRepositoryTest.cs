using HotelSOL.Modelo;
using Xunit;

namespace HotelSOL.Tests.ReservaTest
{
    public class ReservaRepositoryTest
    {
        [Fact]
        public void CrearReserva_Valida_NoLanzaExcepcion()
        {
            var reserva = new Reserva
            {
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2),
                ReservaHabitaciones = new List<ReservaHabitacion>
                {
                    new ReservaHabitacion
                    {
                        Habitacion = new Habitacion { PrecioBase = 100 }
                    }
                }
            };

            var ex = Record.Exception(() => reserva.CrearReserva());
            Assert.Null(ex);
        }

        [Fact]
        public void CancelarReserva_CambiaEstadoACancelada()
        {
            var reserva = new Reserva
            {
                Estado = new Estado { Nombre = "Pendiente" }
            };

            reserva.CancelarReserva();
            Assert.Equal("Cancelada", reserva.Estado.Nombre);
        }

        [Fact]
        public void ConfirmarReserva_CambiaEstadoAConfirmada()
        {
            var reserva = new Reserva
            {
                Estado = new Estado { Nombre = "Pendiente" }
            };

            reserva.ConfirmarReserva();
            Assert.Equal("Confirmada", reserva.Estado.Nombre);
        }
    }
}
