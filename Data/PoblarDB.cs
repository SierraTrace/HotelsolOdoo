// HotelSOL.Client/PoblarDB.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Client
{
    public static class PoblarDB
    {
        public static async Task InicializarAsync(HotelContext context)
        {
            try
            {
                // Si ya existen usuarios, asumimos que la BD ya está inicializada
                if (await context.Usuarios.AnyAsync())
                {
                    return;
                }

                // 1) Sembrar datos estáticos (Tipos, Estados, etc.)
                var clienteTest = new Cliente { Nombre = "Cliente", Apellido = "Test", Email = "c", Password = "1234", Movil = 600000001, EsVIP = false };
                var recepTest = new Recepcionista { Nombre = "Recep", Apellido = "Test", Email = "r", Password = "1234", Movil = 600000002 };
                var adminTest = new Administrador { Nombre = "Admin", Apellido = "Test", Email = "a", Password = "1234", Movil = 600000003 };
                context.Usuarios.AddRange(clienteTest, recepTest, adminTest);

                var tiposHab = new[]
                {
                    new TipoHabitacion { Nombre = "SUITE" },
                    new TipoHabitacion { Nombre = "INDIVIDUAL" },
                    new TipoHabitacion { Nombre = "DOBLE" },
                    new TipoHabitacion { Nombre = "TRIPLE" },
                    new TipoHabitacion { Nombre = "CUADRUPLE" }
                };
                context.TipoHabitaciones.AddRange(tiposHab);

                var tiposAloj = new[]
                {
                    new TipoAlojamiento { Nombre = "MEDIA_PENSION" },
                    new TipoAlojamiento { Nombre = "PENSION_COMPLETA" },
                    new TipoAlojamiento { Nombre = "TODO_INCLUIDO" }
                };
                context.TipoAlojamientos.AddRange(tiposAloj);

                var tiposRes = new[]
                {
                    new TipoReserva { Nombre = "PREVIA" },
                    new TipoReserva { Nombre = "NO_PREVIA" }
                };
                context.TipoReservas.AddRange(tiposRes);

                var estados = new[]
                {
                    new Estado { Nombre = "Pendiente" },
                    new Estado { Nombre = "Confirmada" },
                    new Estado { Nombre = "Cancelada" },
                    new Estado { Nombre = "No Presentado" }
                };
                context.Estados.AddRange(estados);

                var alta = new TipoTemporada { Nombre = "ALTA" };
                var media = new TipoTemporada { Nombre = "MEDIA" };
                var baja = new TipoTemporada { Nombre = "BAJA" };
                context.TipoTemporadas.AddRange(alta, media, baja);

                var year = DateTime.Today.Year;
                var temporadas = new[]
                {
                    new Temporada(alta,  new DateTime(year, 7,  1), new DateTime(year, 8, 31), 1.5),
                    new Temporada(media, new DateTime(year, 4,  1), new DateTime(year, 6, 30), 1.2),
                    new Temporada(baja,  new DateTime(year, 9,  1), new DateTime(year+1,3, 31), 0.8)
                };
                context.Temporadas.AddRange(temporadas);

                var calendario = new Calendario();
                context.Calendarios.Add(calendario);

                // 2) Sembrar Servicios
                var servicios = new[]
                {
                    new Servicio { Nombre = "Spa",                    Precio = 50, Descripcion = "Acceso al spa y zona de bienestar" },
                    new Servicio { Nombre = "Desayuno Buffet",        Precio = 15, Descripcion = "Desayuno tipo buffet incluido" },
                    new Servicio { Nombre = "Wifi Premium",           Precio = 10, Descripcion = "Conexión Wi-Fi de alta velocidad" },
                    new Servicio { Nombre = "Lavandería",             Precio = 20, Descripcion = "Servicio de lavandería exprés" },
                    new Servicio { Nombre = "Servicio a la habitación", Precio = 30, Descripcion = "Pedido de comidas y bebidas a la habitación" },
                    new Servicio { Nombre = "Gimnasio",               Precio = 0,  Descripcion = "Acceso ilimitado al gimnasio" },
                    new Servicio { Nombre = "Piscina",                Precio = 0,  Descripcion = "Acceso a las piscinas del hotel" },
                    new Servicio { Nombre = "Estacionamiento",        Precio = 5,  Descripcion = "Estacionamiento cubierto" },
                    new Servicio { Nombre = "Alojamiento base", Precio=0, Descripcion= "No hay servicios extras contratados"}
                };
                context.Servicios.AddRange(servicios);

                await context.SaveChangesAsync();

                // 3) Clientes de prueba
                var rnd = new Random();
                var clientes = Enumerable.Range(1, 20)
                    .Select(i => new Cliente
                    {
                        Nombre = $"Cliente{i}",
                        Apellido = $"Test{i}",
                        Email = $"cliente{i}@test.com",
                        Password = "1234",
                        Movil = rnd.Next(600000000, 700000000),
                        EsVIP = i % 5 == 0
                    })
                    .ToList();
                context.Usuarios.AddRange(clientes);
                await context.SaveChangesAsync();

                // 4) Habitaciones
                var habitaciones = Enumerable.Range(1, 20)
                    .Select(i => new Habitacion
                    {
                        Numero = $"H{i:000}",
                        TipoHabitacionId = tiposHab[rnd.Next(tiposHab.Length)].Id,
                        Capacidad = rnd.Next(1, 5),
                        PrecioBase = rnd.Next(50, 200),
                        Disponibilidad = true
                    })
                    .ToList();
                context.Habitaciones.AddRange(habitaciones);
                await context.SaveChangesAsync();

                // 5) Alojamientos
                var alojamientos = Enumerable.Range(1, 20)
                    .Select(i =>
                    {
                        var ta = tiposAloj[rnd.Next(tiposAloj.Length)];
                        return new Alojamiento($"A{i:000}", ta.Id, ta, rnd.Next(50, 200));
                    })
                    .ToList();
                context.Alojamientos.AddRange(alojamientos);
                await context.SaveChangesAsync();

                // 6) Reservas de ejemplo
                var todasReservas = Enumerable.Range(1, 20)
                    .Select(i =>
                    {
                        var cli = clientes[rnd.Next(clientes.Count)];
                        var alj = alojamientos[rnd.Next(alojamientos.Count)];
                        var fe = DateTime.Today.AddDays(rnd.Next(1, 30));
                        var fs = fe.AddDays(rnd.Next(1, 7));
                        return new Reserva
                        {
                            ClienteId = cli.Id,
                            AlojamientoId = alj.Id,
                            TipoReservaId = tiposRes[rnd.Next(tiposRes.Length)].Id,
                            EstadoId = estados[rnd.Next(estados.Length)].Id,
                            FechaEntrada = fe,
                            FechaSalida = fs,
                            CalendarioId = calendario.Id
                        };
                    })
                    .ToList();
                context.Reservas.AddRange(todasReservas);
                await context.SaveChangesAsync();

                // 7) ReservaHabitaciones
                var reservaHabs = todasReservas
                    .Select(r =>
                    {
                        var hab = habitaciones[rnd.Next(habitaciones.Count)];
                        var noches = (r.FechaSalida - r.FechaEntrada).Days;
                        return new ReservaHabitacion
                        {
                            ReservaId = r.Id,
                            HabitacionId = hab.Id,
                            Noches = noches,
                            PrecioPorNoche = hab.PrecioBase
                        };
                    })
                    .ToList();
                context.ReservaHabitaciones.AddRange(reservaHabs);
                await context.SaveChangesAsync();

                // 8) Facturas
                var facturas = todasReservas
                    .Select(r => new Factura
                    {
                        ReservaId = r.Id,
                        FechaEmision = DateTime.Now,
                        Total = rnd.Next(100, 1000),
                        MetodoPago = "Pendiente",
                        Pagada = false
                    })
                    .ToList();
                context.Facturas.AddRange(facturas);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Error poblando la base de datos: " + ex.GetBaseException().Message, ex);
            }
        }
    }
}
