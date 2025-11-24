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
    public class TransaccionRepositorio : ITransaccionRepositorio
    {
        private readonly ContabilidadContext _context;

        public TransaccionRepositorio(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<TransaccionFinanciera> ObtenerPorIdAsync(Guid id)
        {
            return await _context.TransaccionesFinancieras.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TransaccionFinanciera>> ObtenerPorCuentaAsync(Guid cuentaId)
        {
            return await _context.TransaccionesFinancieras
                .Where(t => t.CuentaBancariaId == cuentaId)
                .OrderByDescending(t => t.FechaOperacion)
                .ToListAsync();
        }

        public async Task<TransaccionFinanciera> CrearAsync(TransaccionFinanciera transaccion)
        {
            _context.TransaccionesFinancieras.Add(transaccion);
            await _context.SaveChangesAsync();
            return transaccion;
        }

        public async Task<IEnumerable<TransaccionFinanciera>> ObtenerPorRangoFechasAsync(Guid cuentaId, DateTime inicio, DateTime fin)
        {
            return await _context.TransaccionesFinancieras
                .Where(t => t.CuentaBancariaId == cuentaId && t.FechaOperacion >= inicio && t.FechaOperacion <= fin)
                .OrderByDescending(t => t.FechaOperacion)
                .ToListAsync();
        }
    }
}
