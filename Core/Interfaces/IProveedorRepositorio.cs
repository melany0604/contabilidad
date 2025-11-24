using ContabilidadSantaCruz.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Interfaces
{
    public interface IProveedorRepositorio
    {
        Task<Proveedor> ObtenerPorIdAsync(Guid id);
        Task<Proveedor> ObtenerPorNitAsync(string nit);
        Task<IEnumerable<Proveedor>> ObtenerTodosAsync();
        Task<Proveedor> CrearAsync(Proveedor proveedor);
        Task<Proveedor> ActualizarAsync(Proveedor proveedor);
    }
}
