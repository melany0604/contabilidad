using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using ContabilidadSantaCruz.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Infraestructura.Repositorios
{
    public class PresupuestoRepositorio : IPresupuestoRepositorio
    {
        private readonly ContabilidadContext _context;

        public PresupuestoRepositorio(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<PresupuestoCentro> ObtenerPorCentroYPeriodoAsync(string centro, string periodo)
        {
            return await _context.Presupuestos
                .FirstOrDefaultAsync(p => p.NombreCentro == centro && p.Periodo == periodo);
        }

        public async Task<IEnumerable<PresupuestoCentro>> ObtenerTodosAsync()
        {
            return await _context.Presupuestos.ToListAsync();
        }

        public async Task<PresupuestoCentro> CrearAsync(PresupuestoCentro presupuesto)
        {
            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;
        }

        public async Task<PresupuestoCentro> ActualizarAsync(PresupuestoCentro presupuesto)
        {
            presupuesto.FechaActualizacion = DateTime.UtcNow;
            presupuesto.MontoDisponible = presupuesto.MontoAsignado - presupuesto.MontoGastado;
            _context.Presupuestos.Update(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;
        }
    }
}
