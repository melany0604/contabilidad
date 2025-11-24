using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Servicios
{
    public interface IGestionPagosService
    {
        Task<PagoDTO> CrearPagoAsync(CrearPagoDTO dto);
        Task<PagoDTO> ObtenerPagoAsync(Guid id);
        Task<IEnumerable<PagoDTO>> ObtenerTodosAsync();
        Task<bool> EjecutarPagoAsync(Guid pagoId);
    }

    public class GestionPagosService : IGestionPagosService
    {
        private readonly IPagoRepositorio _pagoRepo;

        public GestionPagosService(IPagoRepositorio pagoRepo)
        {
            _pagoRepo = pagoRepo;
        }

        public async Task<PagoDTO> CrearPagoAsync(CrearPagoDTO dto)
        {
            var pago = new Pago
            {
                Tipo = dto.Tipo,
                Concepto = dto.Concepto,
                Monto = dto.Monto,
                Beneficiario = dto.Beneficiario,
                CuentaBancariaDestino = dto.CuentaBancariaDestino,
                Estado = "Autorizado"
            };

            await _pagoRepo.CrearAsync(pago);

            return new PagoDTO
            {
                Id = pago.Id,
                Tipo = pago.Tipo,
                Concepto = pago.Concepto,
                Monto = pago.Monto,
                Beneficiario = pago.Beneficiario,
                Estado = pago.Estado,
                FechaAutorizacion = pago.FechaAutorizacion
            };
        }

        public async Task<PagoDTO> ObtenerPagoAsync(Guid id)
        {
            var pago = await _pagoRepo.ObtenerPorIdAsync(id);
            if (pago == null) throw new Exception("Pago no encontrado");

            return new PagoDTO
            {
                Id = pago.Id,
                Tipo = pago.Tipo,
                Concepto = pago.Concepto,
                Monto = pago.Monto,
                Beneficiario = pago.Beneficiario,
                Estado = pago.Estado,
                FechaAutorizacion = pago.FechaAutorizacion
            };
        }

        public async Task<IEnumerable<PagoDTO>> ObtenerTodosAsync()
        {
            var pagos = await _pagoRepo.ObtenerTodosAsync();
            var resultado = new List<PagoDTO>();

            foreach (var pago in pagos)
            {
                resultado.Add(new PagoDTO
                {
                    Id = pago.Id,
                    Tipo = pago.Tipo,
                    Concepto = pago.Concepto,
                    Monto = pago.Monto,
                    Beneficiario = pago.Beneficiario,
                    Estado = pago.Estado,
                    FechaAutorizacion = pago.FechaAutorizacion
                });
            }

            return resultado;
        }

        public async Task<bool> EjecutarPagoAsync(Guid pagoId)
        {
            var pago = await _pagoRepo.ObtenerPorIdAsync(pagoId);
            if (pago == null) return false;

            pago.Estado = "Ejecutado";
            pago.FechaEjecucion = DateTime.UtcNow;
            pago.NumeroTransferencia = GenerarNumeroTransferencia();

            await _pagoRepo.ActualizarAsync(pago);
            return true;
        }

        private string GenerarNumeroTransferencia()
        {
            return $"TRF-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6).ToUpper()}";
        }
    }
}
