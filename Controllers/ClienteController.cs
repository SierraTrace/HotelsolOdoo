using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _repositorio;

        public ClienteController(IClienteRepository repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("disponibilidad")]
        public async Task<IActionResult> ConsultarDisponibilidad([FromQuery] DateTime entrada, [FromQuery] DateTime salida)
        {
            var resultado = await _repositorio.ConsultarDisponibilidadAsync(entrada, salida);
            return Ok(resultado);
        }

        [HttpPost("reserva")]
        public async Task<IActionResult> RealizarReserva([FromBody] Reserva reserva)
        {
            var resultado = await _repositorio.RealizarReservaAsync(reserva);
            return resultado ? Ok("Reserva realizada correctamente.") : BadRequest("Error al realizar la reserva.");
        }

        [HttpPut("reserva")]
        public async Task<IActionResult> ModificarReserva([FromBody] Reserva reserva)
        {
            var resultado = await _repositorio.ModificarReservaAsync(reserva);
            return resultado ? Ok("Reserva modificada correctamente.") : NotFound("Reserva no encontrada.");
        }

        [HttpDelete("reserva/{id}")]
        public async Task<IActionResult> AnularReserva(int id)
        {
            var resultado = await _repositorio.AnularReservaAsync(id);
            return resultado ? Ok("Reserva anulada correctamente.") : NotFound("Reserva no encontrada.");
        }

        [HttpGet("historial/{clienteId}")]
        public async Task<IActionResult> ConsultarHistorialEstancias(int clienteId)
        {
            var historial = await _repositorio.ObtenerHistorialEstanciasAsync(clienteId);
            return Ok(historial);
        }

        [HttpGet("facturas/{clienteId}")]
        public async Task<IActionResult> ConsultarFacturas(int clienteId)
        {
            var facturas = await _repositorio.ConsultarFacturasAsync(clienteId);
            return Ok(facturas);
        }

        [HttpPost("pago")]
        public async Task<IActionResult> RealizarPago([FromQuery] int facturaId, [FromQuery] string metodoPago)
        {
            var resultado = await _repositorio.RealizarPagoAsync(facturaId, metodoPago);
            return resultado ? Ok("Pago realizado correctamente.") : NotFound("Factura no encontrada.");
        }


        [HttpPost("servicio")]
        public async Task<IActionResult> SolicitarServicio([FromQuery] int clienteId, [FromQuery] string servicioId)
        {
            var resultado = await _repositorio.SolicitarServicioParaClienteAsync(clienteId, servicioId);
            return resultado
                ? Ok("Servicio agregado a la factura correctamente.")
                : BadRequest("Error al agregar el servicio. Verifica si el cliente tiene reservas activas.");
        }

    }
}
