// FacturaRepository.cs
using HotelSOL.Data;
using HotelSOL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace HotelSOL.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly HotelContext _context;

        public FacturaRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Factura>> GetAllAsync()
        {
            return await _context.Facturas
                .Include(f => f.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Include(f => f.Reserva)
                .ToListAsync();
        }

        public async Task<Factura> GetByIdAsync(string id)
        {
            return await _context.Facturas
                .Include(f => f.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Include(f => f.Reserva)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AddAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Factura factura)
        {
            _context.Entry(factura).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var factura = await GetByIdAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> CrearFacturaConServiciosAsync(Factura factura)
        {
            if (factura == null || factura.Detalles == null || !factura.Detalles.Any())
                return false;

            double total = 0;

            foreach (var detalle in factura.Detalles)
            {
                var servicio = await _context.Servicios.FindAsync(detalle.ServicioId);
                if (servicio == null) continue;

                detalle.PrecioUnitario = servicio.Precio;
                detalle.Factura = factura;
                total += detalle.Cantidad * servicio.Precio;
            }

            factura.Total = total;
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Factura>> ObtenerFacturasPorClienteAsync(int clienteId)
        {
            return await _context.Facturas
                .Include(f => f.Reserva)
                .Where(f => f.Reserva.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<Factura> GenerarFacturaParaClienteAsync(int clienteId)
        {
            var reserva = await _context.Reservas
                .Include(r => r.ReservaHabitaciones).ThenInclude(rh => rh.Habitacion)
                .FirstOrDefaultAsync(r => r.ClienteId == clienteId && !_context.Facturas.Any(f => f.ReservaId == r.Id));

            if (reserva == null)
                throw new Exception("No existe una reserva disponible para generar factura.");

            // CORRECCIÓN: usar FechaEmision en lugar de Fecha
            var factura = new Factura
            {
                ReservaId = reserva.Id,
                FechaEmision = DateTime.Now,
                Total = reserva.ReservaHabitaciones.Sum(rh => rh.Noches * rh.PrecioPorNoche),
                Pagada = false
            };

            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            return factura;
        }

        public async Task<string> ObtenerDetalleFacturaAsync(string facturaId)
        {
            var factura = await GetByIdAsync(facturaId);

            if (factura == null)
                throw new Exception("Factura no encontrada.");

            var detalle = $"Factura ID: {factura.Id}\n" +
                          $"Fecha: {factura.FechaEmision}\n" +
                          $"Total: {factura.Total:C}\n\n";

            detalle += "Detalles:\n";
            foreach (var d in factura.Detalles)
            {
                detalle += $"{d.Servicio.Nombre} - Cantidad: {d.Cantidad} - Precio Unitario: {d.PrecioUnitario:C}\n";
            }

            return detalle;
        }
    }
}
