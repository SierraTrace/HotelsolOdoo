/*using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using HotelSOL.Controllers;
using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Tests.AdministradorTest
{
    public class AdministradorControllerTests
    {
        [Fact]
        public async Task ObtenerUsuarios_DeberiaRetornarListaUsuarios()
        {
            var mockRepo = new Mock<IAdministradorRepository>();
            mockRepo.Setup(r => r.ObtenerTodosLosUsuariosAsync())
                .ReturnsAsync(new List<Usuario>
                {
                    new Usuario { Id = 1, Nombre = "Eva" },
                    new Usuario { Id = 2, Nombre = "Luis" }
                });

            var controller = new AdministradorController(mockRepo.Object);

            var resultado = await controller.ObtenerUsuarios();
            var ok = resultado as OkObjectResult;
            var usuarios = ok.Value as List<Usuario>;

            Assert.Equal(2, usuarios.Count);
        }

        [Fact]
        public async Task CrearUsuario_DeberiaRetornarOkSiSeCrea()
        {
            var mockRepo = new Mock<IAdministradorRepository>();
            mockRepo.Setup(r => r.CrearUsuarioAsync(It.IsAny<Usuario>())).ReturnsAsync(true);

            var controller = new AdministradorController(mockRepo.Object);
            var nuevo = new Usuario { Nombre = "Nuevo" };

            var resultado = await controller.CrearUsuario(nuevo);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task ActualizarUsuario_DeberiaRetornarNotFoundSiNoExiste()
        {
            var mockRepo = new Mock<IAdministradorRepository>();
            mockRepo.Setup(r => r.ActualizarUsuarioAsync(It.IsAny<Usuario>())).ReturnsAsync(false);

            var controller = new AdministradorController(mockRepo.Object);
            var usuario = new Usuario { Id = 99 };

            var resultado = await controller.ActualizarUsuario(usuario);

            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task EliminarUsuario_DeberiaRetornarOkSiEliminado()
        {
            var mockRepo = new Mock<IAdministradorRepository>();
            mockRepo.Setup(r => r.EliminarUsuarioAsync(3)).ReturnsAsync(true);

            var controller = new AdministradorController(mockRepo.Object);

            var resultado = await controller.EliminarUsuario(3);

            Assert.IsType<OkObjectResult>(resultado);
        }

        [Fact]
        public async Task ConfigurarSistema_DeberiaRetornarOkSiTodoVaBien()
        {
            var mockRepo = new Mock<IAdministradorRepository>();
            mockRepo.Setup(r => r.GuardarConfiguracionSistemaAsync(It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(true);

            var controller = new AdministradorController(mockRepo.Object);
            var configuracion = new Dictionary<string, string>
            {
                { "Tema", "Oscuro" },
                { "Idioma", "Español" }
            };

            var resultado = await controller.ConfigurarSistema(configuracion);

            Assert.IsType<OkObjectResult>(resultado);
        }
    }
}
*/