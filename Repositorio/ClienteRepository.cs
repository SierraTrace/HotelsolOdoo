using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelSOL.Data;
using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelSOL.Repositorio
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly HotelContext _context;
        public ClienteRepository(HotelContext context) => _context = context;

        public async Task<Cliente?> AutenticarClienteAsync(string email, string password)
            => await _context.Usuarios.OfType<Cliente>()
                .FirstOrDefaultAsync(c => c.Email == email && c.Password == password);

        public async Task<List<Habitacion>> ConsultarDisponibilidadAsync(DateTime entrada, DateTime salida)
            => await _context.Habitaciones
                .Where(h => !_context.ReservaHabitaciones
                    .Any(rh => rh.HabitacionId == h.Id &&
                               rh.Reserva.FechaEntrada < salida &&
                               rh.Reserva.FechaSalida > entrada))
                .ToListAsync();

        public async Task<bool> RealizarReservaConHabitacionAsync(Reserva reserva, int habitacionId)
        {
            try
            {
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();

                int noches = (reserva.FechaSalida - reserva.FechaEntrada).Days;
                var habit = await _context.Habitaciones.FindAsync(habitacionId)
                            ?? throw new InvalidOperationException($"Habitación {habitacionId} no encontrada.");

                _context.ReservaHabitaciones.Add(new ReservaHabitacion
                {
                    ReservaId = reserva.Id,
                    HabitacionId = habitacionId,
                    Noches = noches,
                    PrecioPorNoche = habit.PrecioBase
                });
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RealizarReservaAsync(Reserva reserva)
        {
            try
            {
                _context.Reservas.Add(reserva);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Reserva>> ConsultarReservasAsync(int clienteId)
            => await _context.Reservas
                .Where(r => r.ClienteId == clienteId)
                .Include(r => r.Estado)
                .Include(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .ToListAsync();

        public async Task<List<Reserva>> ConsultarReservasConDetallesAsync(int clienteId)
            => await _context.Reservas
                .Where(r => r.ClienteId == clienteId)
                .Include(r => r.Estado)
                .Include(r => r.TipoReserva)
                .Include(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .Include(r => r.Servicios).ThenInclude(rs => rs.Servicio)
                .ToListAsync();

        public async Task<List<Reserva>> ConsultarReservasActivasAsync(int clienteId)
            => await _context.Reservas
                .Where(r => r.ClienteId == clienteId && r.FechaSalida >= DateTime.Today)
                .Include(r => r.Estado)
                .Include(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .ToListAsync();

        public async Task<bool> ModificarReservaAsync(Reserva reserva)
        {
            try
            {
                _context.Reservas.Update(reserva);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AnularReservaAsync(int reservaId)
        {
            var r = await _context.Reservas.FindAsync(reservaId);
            if (r == null) return false;
            _context.Reservas.Remove(r);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HistoricoEstancia>> ObtenerHistorialEstanciasAsync(int clienteId)
            => await _context.HistoricoEstancias
                .Where(h => h.ClienteId == clienteId)
                .ToListAsync();

        public async Task<List<Factura>> ConsultarFacturasAsync(int clienteId)
            => await _context.Facturas
                .Include(f => f.Reserva).ThenInclude(r => r.ReservaHabitaciones)
                .Where(f => f.Reserva.ClienteId == clienteId)
                .ToListAsync();

        public async Task<List<Factura>> ConsultarFacturasConDetallesAsync(int clienteId)
            => await _context.Facturas
                .Include(f => f.Reserva).ThenInclude(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .Include(f => f.Detalles).ThenInclude(dfs => dfs.Servicio)
                .Where(f => f.Reserva.ClienteId == clienteId)
                .ToListAsync();

        public async Task<List<Factura>> ConsultarFacturasPendientesAsync(int clienteId)
            => await _context.Facturas
                .Include(f => f.Reserva)
                .Where(f => f.Reserva.ClienteId == clienteId && !f.Pagada)
                .ToListAsync();

        public async Task<bool> RealizarPagoAsync(int facturaId, string metodoPago)
        {
            // Convertimos a string para buscar en la clave primaria de tipo string
            var key = facturaId.ToString();
            var f = await _context.Facturas.FindAsync(key);
            if (f == null)
                return false;

            f.MetodoPago = metodoPago;
            f.Pagada = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SolicitarServicioParaReservaAsync(int reservaId, string servicioId)
        {
            var fact = await _context.Facturas.FirstOrDefaultAsync(f => f.ReservaId == reservaId);
            if (fact == null)
            {
                fact = new Factura { ReservaId = reservaId, FechaEmision = DateTime.Now, Total = 0 };
                _context.Facturas.Add(fact);
                await _context.SaveChangesAsync();
            }

            var serv = await _context.Servicios.FindAsync(servicioId);
            if (serv == null)
                return false;

            fact.Total += serv.Precio;
            _context.DetalleFacturaServicios.Add(new DetalleFacturaServicio
            {
                ServicioId = serv.Id,
                FacturaId = fact.Id,
                Cantidad = 1,
                PrecioUnitario = serv.Precio
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SolicitarServicioParaClienteAsync(int clienteId, string servicioId)
        {
            var res = await _context.Reservas
                .Where(r => r.ClienteId == clienteId && r.FechaEntrada <= DateTime.Today && r.FechaSalida >= DateTime.Today)
                .OrderByDescending(r => r.FechaEntrada)
                .FirstOrDefaultAsync();
            if (res == null)
                return false;

            return await SolicitarServicioParaReservaAsync(res.Id, servicioId);
        }

        public async Task<List<Servicio>> ObtenerServiciosAsync()
            => await _context.Servicios.ToListAsync();

        public async Task<List<TipoReserva>> ObtenerTiposReservaAsync()
            => await _context.TipoReservas.ToListAsync();
    }
}
