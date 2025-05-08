using HotelSOL.Modelo;
using HotelSOL.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelSOL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecepcionistaController : ControllerBase
    {
        private readonly IRecepcionistaRepository _repositorio;

        public RecepcionistaController(IRecepcionistaRepository repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpPost("cliente")]
        public async Task<IActionResult> RegistrarCliente([FromBody] Cliente cliente)
        {
            var resultado = await _repositorio.RegistrarClienteAsync(cliente);
            return resultado ? Ok("Cliente registrado correctamente.") : BadRequest("Error al registrar el cliente.");
        }
        
        [HttpPut("actualizarCliente")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] Cliente cliente)
        {
            var actualizado = await _repositorio.ActualizarClienteAsync(cliente);
            if (actualizado)
                return Ok("Cliente actualizado correctamente.");
            return NotFound("Cliente no encontrado.");
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
            return resultado ? Ok("Reserva actualizada correctamente.") : NotFound("Reserva no encontrada.");
        }

        [HttpDelete("reserva/{id}")]
        public async Task<IActionResult> AnularReserva(int id)
        {
            var resultado = await _repositorio.AnularReservaAsync(id);
            return resultado ? Ok("Reserva anulada correctamente.") : NotFound("Reserva no encontrada.");
        }

        [HttpGet("disponibilidad")]
        public async Task<IActionResult> ConsultarDisponibilidad([FromQuery] DateTime entrada, [FromQuery] DateTime salida)
        {
            var habitaciones = await _repositorio.ConsultarDisponibilidadAsync(entrada, salida);
            return Ok(habitaciones);
        }

        [HttpGet("entradas")]
        public async Task<IActionResult> ConsultarEntradas([FromQuery] DateTime fecha)
        {
            var entradas = await _repositorio.ConsultarEntradasAsync(fecha);
            return Ok(entradas);
        }

        [HttpGet("salidas")]
        public async Task<IActionResult> ConsultarSalidas([FromQuery] DateTime fecha)
        {
            var salidas = await _repositorio.ConsultarSalidasAsync(fecha);
            return Ok(salidas);
        }

        [HttpPost("firmar-entrada/{reservaId}")]
        public async Task<IActionResult> FirmarEntrada(int reservaId)
        {
            var resultado = await _repositorio.FirmarEntradaAsync(reservaId);
            return resultado ? Ok("Entrada firmada correctamente.") : NotFound("Reserva no encontrada.");
        }


        [HttpGet("clientes")]
        public async Task<IActionResult> ConsultarListadoClientes()
        {
            var clientes = await _repositorio.ObtenerClientesAsync();
            return Ok(clientes);
        }

        [HttpGet("historico/{clienteId}")]
        public async Task<IActionResult> ConsultarHistoricoEstancias(int clienteId)
        {
            var historico = await _repositorio.ObtenerHistoricoClienteAsync(clienteId);
            return Ok(historico);
        }

        [HttpPost("factura")]
        public async Task<IActionResult> CrearFacturaParaCliente(int clienteId)
        {
            var reserva = await _repositorio.ObtenerReservaActualDelClienteAsync(clienteId);
            if (reserva == null)
                return NotFound("No se encontró una reserva válida para este cliente.");

            var factura = new Factura
            {
                ReservaId = reserva.Id, 
                FechaEmision = DateTime.Now,
                Total = 100
            };

            var creado = await _repositorio.CrearFacturaAsync(factura);

            return creado
                ? Ok("Factura creada correctamente.")
                : BadRequest("Error al crear la factura.");
        }



        [HttpGet("facturas/{clienteId}")]
        public async Task<IActionResult> ConsultarFactura(int clienteId)
        {
            var facturas = await _repositorio.ConsultarFacturasClienteAsync(clienteId);
            return Ok(facturas);
        }

        [HttpPost("pago")]
        public async Task<IActionResult> RealizarPago([FromQuery] string facturaId, [FromQuery] string metodoPago)
        {
            var resultado = await _repositorio.RegistrarPagoAsync(facturaId, metodoPago);
            return resultado ? Ok("Pago registrado correctamente.") : NotFound("Factura no encontrada.");
        }


        [HttpPost("servicio")]
        public async Task<IActionResult> SolicitarServicio([FromQuery] int clienteId, [FromQuery] string servicioId)
        {
            var resultado = await _repositorio.SolicitarServicioParaClienteAsync(clienteId, servicioId);
            return resultado ? Ok("Servicio solicitado correctamente.") : BadRequest("Error al solicitar el servicio.");
        }

        [HttpGet("tiposreserva")]
        public async Task<ActionResult<List<TipoReserva>>> GetTiposReserva()
        {
            var tipos = await _repositorio.ObtenerTiposReservaAsync();
            return Ok(tipos);
        }

        [HttpGet("alojamientos")]
        public async Task<ActionResult<List<Alojamiento>>> GetAlojamientos()
        {
            var alojamientos = await _repositorio.ObtenerAlojamientosAsync();
            return Ok(alojamientos);
        }

        [HttpGet("estadosreserva")]
        public async Task<ActionResult<List<Estado>>> GetEstadosReserva()
        {
            var estados = await _repositorio.ObtenerEstadosReservaAsync();
            return Ok(estados);
        }
    }
}
