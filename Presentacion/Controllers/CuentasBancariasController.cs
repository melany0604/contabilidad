using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Servicios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Presentacion.Controllers
{
    [ApiController]
    [Route("api/contabilidad/santa-cruz/cuentas-bancarias")]
    public class CuentasBancariasController : ControllerBase
    {
        private readonly IGestionCuentasBancariasService _service;

        public CuentasBancariasController(IGestionCuentasBancariasService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCuenta(Guid id)
        {
            try
            {
                var cuenta = await _service.ObtenerCuentaAsync(id);
                return Ok(cuenta);
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
                var cuentas = await _service.ObtenerTodasAsync();
                return Ok(cuentas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearCuenta([FromBody] CrearCuentaBancariaDTO dto)
        {
            try
            {
                var cuenta = await _service.CrearCuentaAsync(dto);
                return CreatedAtAction(nameof(ObtenerCuenta), new { id = cuenta.Id }, cuenta);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/transacciones")]
        public async Task<IActionResult> RegistrarTransaccion(Guid id, [FromBody] CrearTransaccionDTO dto)
        {
            try
            {
                await _service.RegistrarTransaccionAsync(id, dto);
                return Ok(new { mensaje = "Transacción registrada exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
