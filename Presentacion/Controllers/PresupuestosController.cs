using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Presentacion.Controllers
{
    [ApiController]
    [Route("api/contabilidad/santa-cruz/presupuestos")]
    public class PresupuestosController : ControllerBase
    {
        private readonly IGestionPresupuestosService _service;

        public PresupuestosController(IGestionPresupuestosService service)
        {
            _service = service;
        }

        [HttpGet("{centro}/{periodo}")]
        public async Task<IActionResult> ObtenerPresupuesto(string centro, string periodo)
        {
            try
            {
                var presupuesto = await _service.ObtenerPresupuestoAsync(centro, periodo);
                return Ok(presupuesto);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            try
            {
                var presupuestos = await _service.ObtenerTodosAsync();
                return Ok(presupuestos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearPresupuesto([FromBody] CrearPresupuestoDTO dto)
        {
            try
            {
                var presupuesto = await _service.CrearPresupuestoAsync(dto);
                return CreatedAtAction(nameof(ObtenerPresupuesto), new { centro = dto.NombreCentro, periodo = dto.Periodo }, presupuesto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("validar")]
        public async Task<IActionResult> ValidarDisponibilidad([FromBody] dynamic request)
        {
            try
            {
                string centro = request.centro;
                string periodo = request.periodo;
                decimal monto = request.monto;

                bool disponible = await _service.ValidarDisponibilidadAsync(centro, periodo, monto);
                return Ok(new { disponible });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
