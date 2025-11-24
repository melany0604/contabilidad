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
    public class PagoRepositorio : IPagoRepositorio
    {
        private readonly ContabilidadContext _context;

        public PagoRepositorio(ContabilidadContext context)
        {
            _context = context;
        }

        public async Task<Pago> ObtenerPorIdAsync(Guid id)
        {
            return await _context.Pagos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Pago>> ObtenerTodosAsync()
        {
            return await _context.Pagos.OrderByDescending(p => p.FechaAutorizacion).ToListAsync();
        }

        public async Task<IEnumerable<Pago>> ObtenerPorTipoAsync(string tipo)
        {
            return await _context.Pagos
                .Where(p => p.Tipo == tipo)
                .OrderByDescending(p => p.FechaAutorizacion)
                .ToListAsync();
        }

        public async Task<Pago> CrearAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago;
        }

        public async Task<Pago> ActualizarAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
            return pago;
        }
    }
}
