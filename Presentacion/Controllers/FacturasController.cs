using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Presentacion.Controllers
{
    [ApiController]
    [Route("api/contabilidad/santa-cruz/facturas")]
    public class FacturasController : ControllerBase
    {
        private readonly IGestionFacturasService _service;

        public FacturasController(IGestionFacturasService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> EmitirFactura([FromBody] CrearFacturaDTO dto)
        {
            try
            {
                var factura = await _service.EmitirFacturaAsync(dto);
                return CreatedAtAction(nameof(ObtenerFactura), new { id = factura.Id }, factura);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerFactura(Guid id)
        {
            try
            {
                var factura = await _service.ObtenerFacturaAsync(id);
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("cliente/{ci}")]
        public async Task<IActionResult> ObtenerFacturasCliente(string ci)
        {
            try
            {
                var facturas = await _service.ObtenerFacturasClienteAsync(ci);
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            try
            {
                var facturas = await _service.ObtenerTodasAsync();
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/pagar")]
        public async Task<IActionResult> RegistrarPago(Guid id)
        {
            try
            {
                await _service.RegistrarPagoFacturaAsync(id);
                return Ok(new { mensaje = "Factura pagada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

