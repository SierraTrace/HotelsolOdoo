using HotelSOL.Modelo;
using HotelSOL.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaRepository _facturaRepository;

        public FacturaController(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFacturas()
        {
            var facturas = await _facturaRepository.GetAllAsync();
            return Ok(facturas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Factura>> GetFactura(string id)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            if (factura == null)
                return NotFound();
            return Ok(factura);
        }

        [HttpPost]
        public async Task<ActionResult> PostFactura(Factura factura)
        {
            await _facturaRepository.AddAsync(factura);
            return CreatedAtAction(nameof(GetFactura), new { id = factura.Id }, factura);
        }

        [HttpPost("completa")]
        public async Task<IActionResult> CrearFacturaConServicios([FromBody] Factura factura)
        {
            var repo = _facturaRepository as FacturaRepository; // Asegúrate que tengas acceso a CrearFacturaConServiciosAsync

            var resultado = await repo.CrearFacturaConServiciosAsync(factura);
            return resultado ? Ok("Factura creada con servicios.") : BadRequest("Error al crear factura.");
        }

    }
}
