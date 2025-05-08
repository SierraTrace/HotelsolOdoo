using Xunit;
using Moq;
using HotelSOL.Controllers;
using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Tests.ClienteTest
{
    public class ClienteControllerTests
    {
        [Fact]
        public async Task ConsultarDisponibilidad_DeberiaRetornarListaHabitaciones()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ConsultarDisponibilidadAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(new List<Habitacion>
                {
                    new Habitacion { Id = 1, Numero = "101" },
                    new Habitacion { Id = 2, Numero = "102" }
                });

            var controller = new ClienteController(mockRepo.Object);

            var resultado = await controller.ConsultarDisponibilidad(DateTime.Today, DateTime.Today.AddDays(1));
            var ok = resultado as OkObjectResult;
            var lista = ok.Value as List<Habitacion>;

            Assert.NotNull(ok);
            Assert.Equal(2, lista.Count);
        }

        [Fact]
        public async Task RealizarReserva_DeberiaRetornarOkSiSeGuarda()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.RealizarReservaAsync(It.IsAny<Reserva>())).ReturnsAsync(true);

            var controller = new ClienteController(mockRepo.Object);
            var reserva = new Reserva { ClienteId = 1 };

            var resultado = await controller.RealizarReserva(reserva);
            var ok = resultado as OkObjectResult;

            Assert.Equal(200, ok.StatusCode);
        }

        [Fact]
        public async Task ModificarReserva_DeberiaRetornarOkSiSeActualiza()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ModificarReservaAsync(It.IsAny<Reserva>())).ReturnsAsync(true);

            var controller = new ClienteController(mockRepo.Object);
            var reserva = new Reserva { Id = 2 };

            var resultado = await controller.ModificarReserva(reserva);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task AnularReserva_DeberiaRetornarNotFoundSiNoExiste()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.AnularReservaAsync(It.IsAny<int>())).ReturnsAsync(false);

            var controller = new ClienteController(mockRepo.Object);

            var resultado = await controller.AnularReserva(99);

            Assert.IsType<NotFoundResult>(resultado);
        }

        [Fact]
        public async Task ConsultarFacturas_DeberiaRetornarFacturasDelCliente()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.ConsultarFacturasAsync(1))
                .ReturnsAsync(new List<Factura>
                {
                    new Factura { Id = "f1", Reserva = new Reserva { ClienteId = 1 } },
                    new Factura { Id = "f2", Reserva = new Reserva { ClienteId = 1 } }
                });

            var controller = new ClienteController(mockRepo.Object);

            var resultado = await controller.ConsultarFacturas(1);
            var ok = resultado as OkObjectResult;
            var lista = ok.Value as List<Factura>;

            Assert.NotNull(ok);
            Assert.Equal(2, lista.Count);
        }

        [Fact]
        public async Task RealizarPago_DeberiaRetornarOkSiSeCompleta()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.RealizarPagoAsync(1, "Tarjeta")).ReturnsAsync(true);

            var controller = new ClienteController(mockRepo.Object);

            var resultado = await controller.RealizarPago(1, "Tarjeta");

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task SolicitarServicio_DeberiaRetornarOkSiSeAgrega()
        {
            var mockRepo = new Mock<IClienteRepository>();
            mockRepo.Setup(r => r.SolicitarServicioParaClienteAsync(1, "svc123")).ReturnsAsync(true);

            var controller = new ClienteController(mockRepo.Object);

            var resultado = await controller.SolicitarServicio(1, "svc123");

            Assert.IsType<OkObjectResult>(resultado);
        }
    }
}
