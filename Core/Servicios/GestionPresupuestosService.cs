using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Servicios
{
    public interface IGestionPresupuestosService
    {
        Task<PresupuestoDTO> ObtenerPresupuestoAsync(string centro, string periodo);
        Task<IEnumerable<PresupuestoDTO>> ObtenerTodosAsync();
        Task<PresupuestoDTO> CrearPresupuestoAsync(CrearPresupuestoDTO dto);
        Task<bool> ValidarDisponibilidadAsync(string centro, string periodo, decimal monto);
        Task<bool> ActualizarGastoAsync(string centro, string periodo, decimal monto);
    }

    public class GestionPresupuestosService : IGestionPresupuestosService
    {
        private readonly IPresupuestoRepositorio _presupuestoRepo;

        public GestionPresupuestosService(IPresupuestoRepositorio presupuestoRepo)
        {
            _presupuestoRepo = presupuestoRepo;
        }

        public async Task<PresupuestoDTO> ObtenerPresupuestoAsync(string centro, string periodo)
        {
            var presupuesto = await _presupuestoRepo.ObtenerPorCentroYPeriodoAsync(centro, periodo);
            if (presupuesto == null) throw new Exception("Presupuesto no encontrado");

            return new PresupuestoDTO
            {
                Id = presupuesto.Id,
                NombreCentro = presupuesto.NombreCentro,
                Periodo = presupuesto.Periodo,
                MontoAsignado = presupuesto.MontoAsignado,
                MontoGastado = presupuesto.MontoGastado,
                MontoDisponible = presupuesto.MontoDisponible,
                Estado = presupuesto.Estado
            };
        }

        public async Task<IEnumerable<PresupuestoDTO>> ObtenerTodosAsync()
        {
            var presupuestos = await _presupuestoRepo.ObtenerTodosAsync();
            return presupuestos.Select(p => new PresupuestoDTO
            {
                Id = p.Id,
                NombreCentro = p.NombreCentro,
                Periodo = p.Periodo,
                MontoAsignado = p.MontoAsignado,
                MontoGastado = p.MontoGastado,
                MontoDisponible = p.MontoDisponible,
                Estado = p.Estado
            });
        }

        public async Task<PresupuestoDTO> CrearPresupuestoAsync(CrearPresupuestoDTO dto)
        {
            var presupuesto = new PresupuestoCentro
            {
                NombreCentro = dto.NombreCentro,
                Periodo = dto.Periodo,
                MontoAsignado = dto.MontoAsignado,
                MontoGastado = 0,
                MontoDisponible = dto.MontoAsignado
            };

            await _presupuestoRepo.CrearAsync(presupuesto);

            return new PresupuestoDTO
            {
                Id = presupuesto.Id,
                NombreCentro = presupuesto.NombreCentro,
                Periodo = presupuesto.Periodo,
                MontoAsignado = presupuesto.MontoAsignado,
                MontoGastado = presupuesto.MontoGastado,
                MontoDisponible = presupuesto.MontoDisponible,
                Estado = presupuesto.Estado
            };
        }

        public async Task<bool> ValidarDisponibilidadAsync(string centro, string periodo, decimal monto)
        {
            var presupuesto = await _presupuestoRepo.ObtenerPorCentroYPeriodoAsync(centro, periodo);
            if (presupuesto == null) return false;

            return presupuesto.MontoDisponible >= monto;
        }

        public async Task<bool> ActualizarGastoAsync(string centro, string periodo, decimal monto)
        {
            var presupuesto = await _presupuestoRepo.ObtenerPorCentroYPeriodoAsync(centro, periodo);
            if (presupuesto == null) return false;

            if (presupuesto.MontoDisponible < monto) return false;

            presupuesto.MontoGastado += monto;
            presupuesto.MontoDisponible -= monto;

            await _presupuestoRepo.ActualizarAsync(presupuesto);
            return true;
        }
    }
}
