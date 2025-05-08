// RecepcionistaRepository.cs
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
    public class RecepcionistaRepository : IRecepcionistaRepository
    {
        private readonly HotelContext _context;

        // Exponemos el contexto para que el formulario use _repo.Context
        public HotelContext Context => _context;

        public RecepcionistaRepository(HotelContext ctx) => _context = ctx;

        public async Task<Recepcionista?> AutenticarRecepcionistaAsync(string em, string pw)
            => await _context.Usuarios.OfType<Recepcionista>()
                .FirstOrDefaultAsync(r => r.Email == em && r.Password == pw);

        public async Task<bool> RegistrarClienteAsync(Cliente c)
        {
            try
            {
                _context.Usuarios.Add(c);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> ActualizarClienteAsync(Cliente c)
        {
            var e = await _context.Usuarios.FindAsync(c.Id);
            if (e == null) return false;
            e.Nombre = c.Nombre;
            e.Apellido = c.Apellido;
            e.Email = c.Email;
            e.Password = c.Password;
            e.Movil = c.Movil;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RealizarReservaAsync(Reserva r)
        {
            try
            {
                _context.Reservas.Add(r);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> ModificarReservaAsync(Reserva r)
        {
            try
            {
                _context.Reservas.Update(r);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> AnularReservaAsync(int id)
        {
            var r = await _context.Reservas.FindAsync(id);
            if (r == null) return false;
            _context.Reservas.Remove(r);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Habitacion>> ConsultarDisponibilidadAsync(DateTime e, DateTime s)
            => await _context.Habitaciones
                     .Where(h => !_context.ReservaHabitaciones
                         .Any(rh => rh.HabitacionId == h.Id
                                 && rh.Reserva.FechaEntrada < s
                                 && rh.Reserva.FechaSalida > e))
                     .ToListAsync();

        public async Task<List<object>> ConsultarEntradasAsync(DateTime f)
            => await _context.Reservas
                .Where(r => r.FechaEntrada.Date == f.Date)
                .Select(r => new
                {
                    r.Id,
                    r.ClienteId,
                    r.TipoReservaId,
                    r.AlojamientoId,
                    r.EstadoId,
                    r.FechaEntrada,
                    r.FechaSalida
                })
                .ToListAsync<object>();

        public async Task<List<Reserva>> ConsultarSalidasAsync(DateTime f)
            => await _context.Reservas
                .Where(r => r.FechaSalida.Date == f.Date)
                .ToListAsync();

        public async Task<bool> FirmarEntradaAsync(int id)
        {
            var r = await _context.Reservas.Include(r => r.Estado)
                                            .FirstOrDefaultAsync(r => r.Id == id);
            var est = await _context.Estados.FirstOrDefaultAsync(e => e.Nombre == "Confirmada");
            if (r == null || est == null) return false;
            r.EstadoId = est.Id;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cliente>> ObtenerClientesAsync()
            => await _context.Usuarios.OfType<Cliente>().ToListAsync();

        public async Task<List<HistoricoEstancia>> ObtenerHistoricoClienteAsync(int cid)
            => await _context.HistoricoEstancias
                .Where(h => h.ClienteId == cid)
                .ToListAsync();

        public async Task<bool> CrearFacturaAsync(Factura f)
        {
            try
            {
                _context.Facturas.Add(f);
                await _context.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<List<Factura>> ConsultarFacturasClienteAsync(int cid)
            => await _context.Facturas
                .Include(f => f.Reserva)
                .Where(f => f.Reserva.ClienteId == cid)
                .ToListAsync();

        public async Task<bool> RegistrarPagoAsync(string fid, string mp)
        {
            if (!int.TryParse(fid, out var i)) return false;
            var f = await _context.Facturas.FindAsync(i);
            if (f == null) return false;
            f.MetodoPago = mp;
            f.Pagada = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SolicitarServicioParaClienteAsync(int cid, string sid)
            => await new ClienteRepository(_context)
                       .SolicitarServicioParaClienteAsync(cid, sid);

        public async Task<Reserva?> ObtenerReservaActualDelClienteAsync(int cid)
            => await _context.Reservas
                .Where(r => r.ClienteId == cid)
                .OrderByDescending(r => r.FechaEntrada)
                .FirstOrDefaultAsync();

        public async Task<List<Servicio>> ObtenerServiciosAsync()
            => await _context.Servicios.ToListAsync();

        public async Task<List<TipoReserva>> ObtenerTiposReservaAsync()
            => await _context.TipoReservas.ToListAsync();

        public async Task<List<AlojamientoComboDto>> ObtenerAlojamientosAsync()
            => await _context.Alojamientos
                .Include(a => a.Tipo)
                .Select(a => new AlojamientoComboDto
                {
                    Id = a.Id,
                    Nombre = $"{a.Id} - {a.Tipo.Nombre}"
                })
                .ToListAsync();

        public async Task<List<Estado>> ObtenerEstadosReservaAsync()
            => await _context.Estados.ToListAsync();
    }
}
