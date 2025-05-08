/*using Xunit;
using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelSOL.Test.AdministradorTest
{
    public class AdministradorRepositoryTests
    {
        private HotelContext CrearContexto()
        {
            var options = new DbContextOptionsBuilder<HotelContext>()
                .UseInMemoryDatabase(databaseName: "AdminTest_" + Guid.NewGuid())
                .Options;

            return new HotelContext(options);
        }

        [Fact]
        public async Task CrearUsuario_DeberiaAgregarUsuario()
        {
            var context = CrearContexto();
            var repo = new AdministradorRepository(context);

            var usuario = new Usuario
            {
                Nombre = "Admin",
                Apellido = "Test",
                Email = "admin@test.com",
                Password = "admin123",
                Movil = 999111222
            };

            var resultado = await repo.CrearUsuarioAsync(usuario);

            Assert.True(resultado);
            Assert.Single(context.Usuarios);
        }

        [Fact]
        public async Task ObtenerTodosLosUsuarios_DeberiaRetornarLista()
        {
            var context = CrearContexto();
            var repo = new AdministradorRepository(context);

            context.Usuarios.AddRange(
                new Usuario { Nombre = "Uno", Apellido = "A", Email = "u1@test.com", Password = "1", Movil = 111 },
                new Usuario { Nombre = "Dos", Apellido = "B", Email = "u2@test.com", Password = "2", Movil = 222 }
            );

            await context.SaveChangesAsync();

            var resultado = await repo.ObtenerTodosLosUsuariosAsync();

            Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public async Task ActualizarUsuario_DeberiaModificarDatos()
        {
            var context = CrearContexto();
            var repo = new AdministradorRepository(context);

            var usuario = new Usuario { Nombre = "Original", Apellido = "Apellido", Email = "edit@test.com", Password = "123", Movil = 333 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            usuario.Nombre = "Modificado";
            var actualizado = await repo.ActualizarUsuarioAsync(usuario);
            var desdeBD = await context.Usuarios.FindAsync(usuario.Id);

            Assert.True(actualizado);
            Assert.Equal("Modificado", desdeBD.Nombre);
        }

        [Fact]
        public async Task EliminarUsuario_DeberiaQuitarUsuario()
        {
            var context = CrearContexto();
            var repo = new AdministradorRepository(context);

            var usuario = new Usuario { Nombre = "Para borrar", Email = "borrar@test.com", Password = "123", Movil = 444 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var resultado = await repo.EliminarUsuarioAsync(usuario.Id);
            var eliminado = await context.Usuarios.FindAsync(usuario.Id);

            Assert.True(resultado);
            Assert.Null(eliminado);
        }

        [Fact]
        public async Task GuardarConfiguracionSistema_DeberiaSimularGuardado()
        {
            var context = CrearContexto();
            var repo = new AdministradorRepository(context);

            var config = new Dictionary<string, string>
            {
                { "Tema", "Oscuro" },
                { "Idioma", "Español" }
            };

            var resultado = await repo.GuardarConfiguracionSistemaAsync(config);

            // Como es simulado, solo verificamos que devuelva true
            Assert.True(resultado);
        }
    }
}
*/