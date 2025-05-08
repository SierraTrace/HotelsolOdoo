/*using Xunit;
using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HotelSOL.Test.RecepcionistaTest
{
    public class RecepcionistaRepositoryTests
    {
        private HotelContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseInMemoryDatabase("RecepcionistaTest_" + Guid.NewGuid())
                .Options;

            return new HotelContext(options);
        }

        [Fact]
        public async Task RegistrarCliente_DeberiaAgregarCliente()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Apellido = "Ruiz",
                Email = "pedro@test.com",
                Password = "abc123",
                Movil = 123456789
            };

            var resultado = await repo.RegistrarClienteAsync(cliente);

            Assert.True(resultado);
            Assert.Single(context.Clientes);
        }

        [Fact]
        public async Task RealizarReserva_DeberiaGuardarReserva()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var reserva = new Reserva
            {
                ClienteId = 1,
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(1),
                Estado = new Estado { Nombre = "Pendiente" }
            };

            context.Estados.Add(reserva.Estado);
            await context.SaveChangesAsync();

            var resultado = await repo.RealizarReservaAsync(reserva);

            Assert.True(resultado);
            Assert.Single(context.Reservas);
        }

        [Fact]
        public async Task ConsultarDisponibilidad_DeberiaExcluirHabitacionesReservadas()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var h1 = new Habitacion { Id = 1, Numero = "201" };
            var h2 = new Habitacion { Id = 2, Numero = "202" };
            context.Habitaciones.AddRange(h1, h2);

            var reserva = new Reserva
            {
                Id = 1,
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2)
            };

            context.Reservas.Add(reserva);
            context.ReservaHabitaciones.Add(new ReservaHabitacion
            {
                ReservaId = reserva.Id,
                HabitacionId = 1
            });

            await context.SaveChangesAsync();

            var disponibles = await repo.ConsultarDisponibilidadAsync(DateTime.Today, DateTime.Today.AddDays(1));

            Assert.Single(disponibles);
            Assert.Equal("202", disponibles[0].Numero);
        }

        [Fact]
        public async Task ConsultarEntradas_DeberiaDevolverReservasDelDia()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            context.Reservas.AddRange(
                new Reserva { ClienteId = 1, FechaEntrada = DateTime.Today },
                new Reserva { ClienteId = 2, FechaEntrada = DateTime.Today.AddDays(1) }
            );

            await context.SaveChangesAsync();

            var entradas = await repo.ConsultarEntradasAsync(DateTime.Today);

            Assert.Single(entradas);
        }

        [Fact]
        public async Task FirmarEntrada_DeberiaActualizarReserva()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var estadoOriginal = new Estado { Nombre = "Pendiente" };
            var estadoConfirmada = new Estado { Nombre = "Confirmada" };
            context.Estados.AddRange(estadoOriginal, estadoConfirmada);

            var reserva = new Reserva
            {
                ClienteId = 1,
                FechaEntrada = DateTime.Today,
                FechaSalida = DateTime.Today.AddDays(2),
                Estado = estadoOriginal
            };

            context.Reservas.Add(reserva);
            await context.SaveChangesAsync();

            var resultado = await repo.FirmarEntradaAsync(reserva.Id);
            var actualizado = await context.Reservas.Include(r => r.Estado).FirstOrDefaultAsync(r => r.Id == reserva.Id);

            Assert.True(resultado);
            Assert.Equal("Confirmada", actualizado.Estado.Nombre);
        }

        [Fact]
        public async Task ConsultarSalidas_DeberiaDevolverReservasConSalidaHoy()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            context.Reservas.AddRange(
                new Reserva { ClienteId = 1, FechaSalida = DateTime.Today },
                new Reserva { ClienteId = 2, FechaSalida = DateTime.Today.AddDays(1) }
            );

            await context.SaveChangesAsync();

            var salidas = await repo.ConsultarSalidasAsync(DateTime.Today);

            Assert.Single(salidas);
        }

        [Fact]
        public async Task CrearFactura_DeberiaGuardarFactura()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var reserva = new Reserva { Id = 1, ClienteId = 1 };
            context.Reservas.Add(reserva);
            await context.SaveChangesAsync();

            var factura = new Factura
            {
                ReservaId = reserva.Id,
                Total = 120
            };

            var resultado = await repo.CrearFacturaAsync(factura);

            Assert.True(resultado);
            Assert.Single(context.Facturas);
        }

        [Fact]
        public async Task ConsultarFacturasCliente_DeberiaDevolverSoloDelCliente()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var r1 = new Reserva { Id = 1, ClienteId = 5 };
            var r2 = new Reserva { Id = 2, ClienteId = 9 };
            context.Reservas.AddRange(r1, r2);

            var f1 = new Factura { ReservaId = r1.Id, Reserva = r1 };
            var f2 = new Factura { ReservaId = r1.Id, Reserva = r1 };
            var f3 = new Factura { ReservaId = r2.Id, Reserva = r2 };
            context.Facturas.AddRange(f1, f2, f3);

            await context.SaveChangesAsync();

            var facturas = await repo.ConsultarFacturasClienteAsync(5);

            Assert.Equal(2, facturas.Count);
        }

        [Fact]
        public async Task RegistrarPago_DeberiaActualizarFactura()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var reserva = new Reserva { Id = 1, ClienteId = 1 };
            context.Reservas.Add(reserva);

            var factura = new Factura { Id = "abc123", Reserva = reserva, MetodoPago = "Pendiente", Pagada = false };
            context.Facturas.Add(factura);
            await context.SaveChangesAsync();

            var resultado = await repo.RegistrarPagoAsync("abc123", "Tarjeta");
            var actualizada = await context.Facturas.FindAsync("abc123");

            Assert.True(resultado);
            Assert.True(actualizada.Pagada);
            Assert.Equal("Tarjeta", actualizada.MetodoPago);
        }

        [Fact]
        public async Task SolicitarServicioParaCliente_DeberiaAgregarDetalleAFactura()
        {
            // Arrange
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            var cliente = new Cliente { Nombre = "Ana" };
            var reserva = new Reserva
            {
                Cliente = cliente,
                FechaEntrada = DateTime.Today.AddDays(-1),
                FechaSalida = DateTime.Today.AddDays(1)
            };
            var servicio = new Servicio { Id = "lavanderia", Nombre = "Lavandería", Precio = 15 };

            context.Clientes.Add(cliente);
            context.Reservas.Add(reserva);
            context.Servicios.Add(servicio);
            await context.SaveChangesAsync();

            // Act
            var resultado = await repo.SolicitarServicioParaClienteAsync(cliente.Id, servicio.Id);

            // Assert
            var factura = await context.Facturas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.ReservaId == reserva.Id);

            Assert.True(resultado);
            Assert.NotNull(factura);
            Assert.Single(factura.Detalles);

            var detalle = factura.Detalles.First();
            Assert.Equal(servicio.Id, detalle.ServicioId);
            Assert.Equal(15, detalle.PrecioUnitario);
        }



        [Fact]
        public async Task ObtenerClientes_DeberiaRetornarTodos()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            context.Clientes.AddRange(
                new Cliente { Nombre = "Mario", Apellido = "López" },
                new Cliente { Nombre = "Laura", Apellido = "Santos" }
            );

            await context.SaveChangesAsync();

            var clientes = await repo.ObtenerClientesAsync();

            Assert.Equal(2, clientes.Count);
        }

        [Fact]
        public async Task ObtenerHistoricoCliente_DeberiaRetornarEstanciasDelCliente()
        {
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            context.HistoricoEstancias.AddRange(
                new HistoricoEstancia { ClienteId = 1, FechaEntrada = DateTime.Today.AddDays(-5), FechaSalida = DateTime.Today },
                new HistoricoEstancia { ClienteId = 2, FechaEntrada = DateTime.Today.AddDays(-3), FechaSalida = DateTime.Today }
            );

            await context.SaveChangesAsync();

            var historial = await repo.ObtenerHistoricoClienteAsync(1);

            Assert.Single(historial);
        }

        [Fact]
        public async Task ObtenerServicios_DeberiaRetornarServicios()
        {
            // Arrange
            var context = CrearContexto();
            var repo = new RecepcionistaRepository(context);

            context.Servicios.AddRange(
                new Servicio { Id = "s1", Nombre = "Spa", Precio = 50 },
                new Servicio { Id = "s2", Nombre = "Desayuno", Precio = 10 }
            );
            await context.SaveChangesAsync();

            // Act
            var servicios = await repo.ObtenerServiciosAsync();

            // Assert
            Assert.Equal(2, servicios.Count);
            Assert.Contains(servicios, s => s.Nombre == "Spa");
            Assert.Contains(servicios, s => s.Nombre == "Desayuno");
        }

    }
}
*/
