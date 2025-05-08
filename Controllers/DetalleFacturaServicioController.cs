using HotelSOL.Modelo;
using HotelSOL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleFacturaServicioController : ControllerBase
    {
        private readonly IDetalleFacturaServicioRepository _detalleRepository;

        public DetalleFacturaServicioController(IDetalleFacturaServicioRepository detalleRepository)
        {
            _detalleRepository = detalleRepository;
        }

        // GET: api/DetalleFacturaServicio
        // Devuelve la lista de detalles, incluyendo la información del servicio y de la factura relacionados.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaServicio>>> GetDetalles()
        {
            var detalles = await _detalleRepository.GetAllAsync();
            return Ok(detalles);
        }

        // POST: api/DetalleFacturaServicio
        // Permite crear un nuevo detalle de factura.
        [HttpPost]
        public async Task<ActionResult<DetalleFacturaServicio>> PostDetalle(DetalleFacturaServicio detalle)
        {
            await _detalleRepository.AddAsync(detalle);
            return CreatedAtAction(nameof(GetDetalles), new { id = detalle.Id }, detalle);
        }
    }
}
