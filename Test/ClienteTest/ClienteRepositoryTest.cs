using Xunit;
using HotelSOL.Modelo;
using HotelSOL.Repositorio;
using HotelSOL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HotelSOL.Test.ClienteTest
{
    public class ClienteRepositoryTests
    {
        private HotelContext CrearContextoEnMemoria()
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseInMemoryDatabase(databaseName: "ClienteTestDb_" + Guid.NewGuid())
                .Options;

            return new HotelContext(options);
        }

        [Fact]
        public async Task RealizarReserva_DeberiaGuardarReserva()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var reserva = new Reserva
            {
                ClienteId = 1,
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2)
            };

            var resultado = await repo.RealizarReservaAsync(reserva);

            Assert.True(resultado);
            Assert.Single(context.Reservas);
        }

        [Fact]
        public async Task ModificarReserva_DeberiaActualizarFechas()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var reserva = new Reserva
            {
                ClienteId = 2,
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(3)
            };

            await repo.RealizarReservaAsync(reserva);
            var guardada = context.Reservas.First();
            guardada.FechaSalida = DateTime.Today.AddDays(5);

            var actualizado = await repo.ModificarReservaAsync(guardada);
            var actual = context.Reservas.First();

            Assert.True(actualizado);
            Assert.Equal(DateTime.Today.AddDays(5), actual.FechaSalida);
        }

        [Fact]
        public async Task AnularReserva_DeberiaEliminarReserva()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var reserva = new Reserva
            {
                ClienteId = 3,
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(1)
            };

            await repo.RealizarReservaAsync(reserva);
            var id = context.Reservas.First().Id;

            var eliminado = await repo.AnularReservaAsync(id);
            var resultado = context.Reservas.Find(id);

            Assert.True(eliminado);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task ConsultarFacturas_DeberiaDevolverFacturasDeCliente()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var clienteId = 10;

            var reserva1 = new Reserva { ClienteId = clienteId };
            var reserva2 = new Reserva { ClienteId = clienteId };
            var reserva3 = new Reserva { ClienteId = 20 };

            context.Reservas.AddRange(reserva1, reserva2, reserva3);
            await context.SaveChangesAsync();

            context.Facturas.AddRange(
                new Factura { Reserva = reserva1 },
                new Factura { Reserva = reserva2 },
                new Factura { Reserva = reserva3 }
            );

            await context.SaveChangesAsync();

            var resultado = await repo.ConsultarFacturasAsync(clienteId);
            Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public async Task RealizarPago_DeberiaActualizarFactura()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var reserva = new Reserva { ClienteId = 99 };
            context.Reservas.Add(reserva);
            await context.SaveChangesAsync();

            var factura = new Factura
            {
                Reserva = reserva,
                MetodoPago = "Pendiente",
                Pagada = false
            };

            context.Facturas.Add(factura);
            await context.SaveChangesAsync();

            var id = factura.Id;
            var resultado = await repo.RealizarPagoAsync(int.Parse(id), "Tarjeta");

            var actualizada = context.Facturas.Find(id);

            Assert.True(resultado);
            Assert.True(actualizada.Pagada);
            Assert.Equal("Tarjeta", actualizada.MetodoPago);
        }

        [Fact]
        public async Task SolicitarServicioParaCliente_DeberiaAgregarDetalleYActualizarFactura()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var clienteId = 1;
            var reserva = new Reserva { ClienteId = clienteId };
            var servicio = new Servicio { Nombre = "Spa", Precio = 100 };

            context.Reservas.Add(reserva);
            context.Servicios.Add(servicio);
            await context.SaveChangesAsync();

            var resultado = await repo.SolicitarServicioParaClienteAsync(clienteId, servicio.Id);

            var detalle = context.DetalleFacturaServicios.FirstOrDefault();

            Assert.True(resultado);
            Assert.NotNull(detalle);
            Assert.Equal(servicio.Precio, detalle.PrecioUnitario);
        }


        [Fact]
        public async Task ObtenerHistorialEstancias_DeberiaRetornarHistorialDelCliente()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            context.HistoricoEstancias.AddRange(
                new HistoricoEstancia { ClienteId = 7, FechaEntrada = DateTime.Today.AddDays(-10), FechaSalida = DateTime.Today.AddDays(-7) },
                new HistoricoEstancia { ClienteId = 7, FechaEntrada = DateTime.Today.AddDays(-5), FechaSalida = DateTime.Today.AddDays(-2) },
                new HistoricoEstancia { ClienteId = 8, FechaEntrada = DateTime.Today.AddDays(-4), FechaSalida = DateTime.Today }
            );

            await context.SaveChangesAsync();

            var historial = await repo.ObtenerHistorialEstanciasAsync(7);

            Assert.Equal(2, historial.Count);
        }

        [Fact]
        public async Task ConsultarDisponibilidad_DeberiaDevolverHabitacionesLibres()
        {
            var context = CrearContextoEnMemoria();
            var repo = new ClienteRepository(context);

            var habitacion1 = new Habitacion { Id = 1, Numero = "101" };
            var habitacion2 = new Habitacion { Id = 2, Numero = "102" };

            context.Habitaciones.AddRange(habitacion1, habitacion2);
            await context.SaveChangesAsync();

            var reserva = new Reserva
            {
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(3)
            };

            var reservaHabitacion = new ReservaHabitacion
            {
                Reserva = reserva,
                Habitacion = habitacion1
            };

            context.Reservas.Add(reserva);
            context.ReservaHabitaciones.Add(reservaHabitacion);
            await context.SaveChangesAsync();

            var disponibles = await repo.ConsultarDisponibilidadAsync(DateTime.Today, DateTime.Today.AddDays(2));

            Assert.Single(disponibles);
            Assert.Equal("102", disponibles[0].Numero);
        }
    }
}
