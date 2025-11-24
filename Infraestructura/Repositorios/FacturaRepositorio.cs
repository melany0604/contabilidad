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
    public class FacturaRepositorio : IFacturaRepositorio
    {
        private readonly ContabilidadContext _context;

        public FacturaRepositorio(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<FacturaEmitida> ObtenerPorIdAsync(Guid id)
        {
            return await _context.FacturasEmitidas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<FacturaEmitida> ObtenerPorNumeroAsync(string numero)
        {
            return await _context.FacturasEmitidas
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.NumeroFactura == numero);
        }

        public async Task<IEnumerable<FacturaEmitida>> ObtenerPorClienteAsync(string ci)
        {
            return await _context.FacturasEmitidas
                .Include(f => f.Detalles)
                .Where(f => f.CiCliente == ci)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }

        public async Task<FacturaEmitida> CrearAsync(FacturaEmitida factura)
        {
            _context.FacturasEmitidas.Add(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<FacturaEmitida> ActualizarAsync(FacturaEmitida factura)
        {
            factura.FechaCreacion = DateTime.UtcNow;
            _context.FacturasEmitidas.Update(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<IEnumerable<FacturaEmitida>> ObtenerTodosAsync()
        {
            return await _context.FacturasEmitidas
                .Include(f => f.Detalles)
                .OrderByDescending(f => f.FechaEmision)
                .ToListAsync();
        }
    }
}
