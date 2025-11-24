using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using ContabilidadSantaCruz.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Infraestructura.Repositorios
{
    public class ProveedorRepositorio : IProveedorRepositorio
    {
        private readonly ContabilidadContext _context;

        public ProveedorRepositorio(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<Proveedor> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Proveedores
                .Include(p => p.CuentasPorPagar)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Proveedor> ObtenerPorNitAsync(string nit)
        {
            return await _context.Proveedores.FirstOrDefaultAsync(p => p.Nit == nit);
        }

        public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }

        public async Task<Proveedor> CrearAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<Proveedor> ActualizarAsync(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
    }
}
