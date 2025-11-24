using ContabilidadSantaCruz.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Interfaces
{
    public interface IPagoRepositorio
    {
        Task<Pago> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<Pago>> ObtenerTodosAsync();
        Task<IEnumerable<Pago>> ObtenerPorTipoAsync(string tipo);
        Task<Pago> CrearAsync(Pago pago);
        Task<Pago> ActualizarAsync(Pago pago);
    }
}
