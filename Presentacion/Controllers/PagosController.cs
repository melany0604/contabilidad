using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Presentacion.Controllers
{
    [ApiController]
    [Route("api/contabilidad/santa-cruz/pagos")]
    public class PagosController : ControllerBase
    {
        private readonly IGestionPagosService _service;

        public PagosController(IGestionPagosService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CrearPago([FromBody] CrearPagoDTO dto)
        {
            try
            {
                var pago = await _service.CrearPagoAsync(dto);
                return CreatedAtAction(nameof(ObtenerPago), new { id = pago.Id }, pago);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPago(Guid id)
        {
            try
            {
                var pago = await _service.ObtenerPagoAsync(id);
                return Ok(pago);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/ejecutar")]
        public async Task<IActionResult> EjecutarPago(Guid id)
        {
            try
            {
                await _service.EjecutarPagoAsync(id);
                return Ok(new { mensaje = "Pago ejecutado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
