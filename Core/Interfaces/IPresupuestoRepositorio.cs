using ContabilidadSantaCruz.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Interfaces
{
    public interface IPresupuestoRepositorio
    {
        Task<PresupuestoCentro> ObtenerPorCentroYPeriodoAsync(string centro, string periodo);
        Task<IEnumerable<PresupuestoCentro>> ObtenerTodosAsync();
        Task<PresupuestoCentro> CrearAsync(PresupuestoCentro presupuesto);
        Task<PresupuestoCentro> ActualizarAsync(PresupuestoCentro presupuesto);
    }
}
