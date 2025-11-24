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
    public class CuentaBancariaRepositorio : ICuentaBancariaRepositorio
    {
        private readonly ContabilidadContext _context;

        public CuentaBancariaRepositorio(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<CuentaBancaria> ObtenerPorIdAsync(Guid id)
        {
            return await _context.CuentasBancarias
                .Include(c => c.Transacciones)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<CuentaBancaria>> ObtenerTodosAsync()
        {
            return await _context.CuentasBancarias
                .Include(c => c.Transacciones)
                .ToListAsync();
        }

        public async Task<CuentaBancaria> CrearAsync(CuentaBancaria cuenta)
        {
            _context.CuentasBancarias.Add(cuenta);
            await _context.SaveChangesAsync();
            return cuenta;
        }

        public async Task<CuentaBancaria> ActualizarAsync(CuentaBancaria cuenta)
        {
            cuenta.FechaActualizacion = DateTime.UtcNow;
            _context.CuentasBancarias.Update(cuenta);
            await _context.SaveChangesAsync();
            return cuenta;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            var cuenta = await ObtenerPorIdAsync(id);
            if (cuenta == null) return false;

            _context.CuentasBancarias.Remove(cuenta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
