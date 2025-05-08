/*using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using HotelSOL.Controllers;
using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Tests.RecepcionistaTest
{
    public class RecepcionistaControllerTests
    {
        [Fact]
        public async Task RegistrarCliente_DeberiaRetornarOk()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.RegistrarClienteAsync(It.IsAny<Cliente>())).ReturnsAsync(true);

            var controller = new RecepcionistaController(mockRepo.Object);
            var cliente = new Cliente { Nombre = "Eva", Email = "eva@test.com" };

            var resultado = await controller.RegistrarCliente(cliente);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task RealizarReserva_DeberiaRetornarBadRequestSiFalla()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.RealizarReservaAsync(It.IsAny<Reserva>())).ReturnsAsync(false);

            var controller = new RecepcionistaController(mockRepo.Object);
            var reserva = new Reserva { ClienteId = 1 };

            var resultado = await controller.RealizarReserva(reserva);

            Assert.IsType<BadRequestObjectResult>(resultado);
        }

        [Fact]
        public async Task ConsultarDisponibilidad_DeberiaRetornarHabitaciones()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.ConsultarDisponibilidadAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Habitacion> { new Habitacion { Numero = "105" } });

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.ConsultarDisponibilidad(DateTime.Today, DateTime.Today.AddDays(1));
            var ok = resultado as OkObjectResult;
            var habitaciones = ok.Value as List<Habitacion>;

            Assert.Single(habitaciones);
            Assert.Equal("105", habitaciones[0].Numero);
        }

        [Fact]
        public async Task ConsultarEntradas_DeberiaRetornarEntradasDelDia()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.ConsultarEntradasAsync(It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Reserva> { new Reserva { ClienteId = 1 } });

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.ConsultarEntradas(DateTime.Today);
            var ok = resultado as OkObjectResult;
            var reservas = ok.Value as List<Reserva>;

            Assert.Single(reservas);
        }

        [Fact]
        public async Task FirmarEntrada_DeberiaRetornarOkSiSeFirma()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.FirmarEntradaAsync(1)).ReturnsAsync(true);

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.FirmarEntrada(1);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task ConsultarListadoClientes_DeberiaRetornarClientes()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.ObtenerClientesAsync())
                .ReturnsAsync(new List<Cliente> { new Cliente { Nombre = "Eva" } });

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.ConsultarListadoClientes();
            var ok = resultado as OkObjectResult;
            var clientes = ok.Value as List<Cliente>;

            Assert.Single(clientes);
        }

        [Fact]
        public async Task RegistrarPago_DeberiaRetornarOkSiCorrecto()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.RegistrarPagoAsync("abc123", "Tarjeta")).ReturnsAsync(true);

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.RealizarPago("abc123", "Tarjeta");

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task SolicitarServicio_DeberiaRetornarOk()
        {
            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.SolicitarServicioParaClienteAsync(1, "spa")).ReturnsAsync(true);

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.SolicitarServicio(1, "spa");

            Assert.IsType<OkObjectResult>(resultado);
        }


        [Fact]
        public async Task CrearFacturaParaCliente_DeberiaRetornarOkSiSeCrea()
        {
            var clienteId = 1;
            var reserva = new Reserva { Id = 42, ClienteId = clienteId };

            var mockRepo = new Mock<IRecepcionistaRepository>();
            mockRepo.Setup(r => r.ObtenerReservaActualDelClienteAsync(clienteId))
                    .ReturnsAsync(reserva);

            mockRepo.Setup(r => r.CrearFacturaAsync(It.IsAny<Factura>()))
                    .ReturnsAsync(true);

            var controller = new RecepcionistaController(mockRepo.Object);

            var resultado = await controller.CrearFacturaParaCliente(clienteId);

            Assert.IsType<OkObjectResult>(resultado);
        }

    }
}
*/