using ContabilidadSantaCruz.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Interfaces
{
    public interface ITransaccionRepositorio
    {
        Task<TransaccionFinanciera> ObtenerPorIdAsync(Guid id);
        Task<IEnumerable<TransaccionFinanciera>> ObtenerPorCuentaAsync(Guid cuentaId);
        Task<TransaccionFinanciera> CrearAsync(TransaccionFinanciera transaccion);
        Task<IEnumerable<TransaccionFinanciera>> ObtenerPorRangoFechasAsync(Guid cuentaId, DateTime inicio, DateTime fin);
    }
}
