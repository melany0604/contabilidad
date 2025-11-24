using ContabilidadSantaCruz.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Interfaces
{
    public interface IFacturaRepositorio
    {
        Task<FacturaEmitida> ObtenerPorIdAsync(Guid id);
        Task<FacturaEmitida> ObtenerPorNumeroAsync(string numero);
        Task<IEnumerable<FacturaEmitida>> ObtenerPorClienteAsync(string ci);
        Task<FacturaEmitida> CrearAsync(FacturaEmitida factura);
        Task<FacturaEmitida> ActualizarAsync(FacturaEmitida factura);
        Task<IEnumerable<FacturaEmitida>> ObtenerTodosAsync();
    }
}
