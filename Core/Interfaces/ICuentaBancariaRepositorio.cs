using ContabilidadSantaCruz.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Interfaces
{
    public interface ICuentaBancariaRepositorio
    {
        Task<CuentaBancaria> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<CuentaBancaria>> ObtenerTodosAsync();
        Task<CuentaBancaria> CrearAsync(CuentaBancaria cuenta);
        Task<CuentaBancaria> ActualizarAsync(CuentaBancaria cuenta);
        Task<bool> EliminarAsync(Guid id);
    }
}
