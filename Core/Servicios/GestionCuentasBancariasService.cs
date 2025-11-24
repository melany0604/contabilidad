using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Servicios
{
    public interface IGestionCuentasBancariasService
    {
        Task<CuentaBancariaDTO> ObtenerCuentaAsync(Guid id);
        Task<IEnumerable<CuentaBancariaDTO>> ObtenerTodasAsync();
        Task<CuentaBancariaDTO> CrearCuentaAsync(CrearCuentaBancariaDTO dto);
        Task<bool> RegistrarTransaccionAsync(Guid cuentaId, CrearTransaccionDTO dto);
    }

    public class GestionCuentasBancariasService : IGestionCuentasBancariasService
    {
        private readonly ICuentaBancariaRepositorio _cuentaRepo;
        private readonly ITransaccionRepositorio _transaccionRepo;

        public GestionCuentasBancariasService(ICuentaBancariaRepositorio cuentaRepo, ITransaccionRepositorio transaccionRepo)
        {
            _cuentaRepo = cuentaRepo;
            _transaccionRepo = transaccionRepo;
        }

        public async Task<CuentaBancariaDTO> ObtenerCuentaAsync(Guid id)
        {
            var cuenta = await _cuentaRepo.ObtenerPorIdAsync(id);
            if (cuenta == null) throw new Exception("Cuenta no encontrada");

            return new CuentaBancariaDTO
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                NombreBanco = cuenta.NombreBanco,
                SaldoActual = cuenta.SaldoActual,
                Moneda = cuenta.Moneda,
                Estado = cuenta.Estado
            };
        }

        public async Task<IEnumerable<CuentaBancariaDTO>> ObtenerTodasAsync()
        {
            var cuentas = await _cuentaRepo.ObtenerTodosAsync();
            return cuentas.Select(c => new CuentaBancariaDTO
            {
                Id = c.Id,
                NumeroCuenta = c.NumeroCuenta,
                NombreBanco = c.NombreBanco,
                SaldoActual = c.SaldoActual,
                Moneda = c.Moneda,
                Estado = c.Estado
            });
        }

        public async Task<CuentaBancariaDTO> CrearCuentaAsync(CrearCuentaBancariaDTO dto)
        {
            var cuenta = new CuentaBancaria
            {
                NumeroCuenta = dto.NumeroCuenta,
                NombreBanco = dto.NombreBanco,
                SaldoActual = dto.SaldoInicial,
                Moneda = dto.Moneda
            };

            await _cuentaRepo.CrearAsync(cuenta);

            return new CuentaBancariaDTO
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                NombreBanco = cuenta.NombreBanco,
                SaldoActual = cuenta.SaldoActual,
                Moneda = cuenta.Moneda,
                Estado = cuenta.Estado
            };
        }

        public async Task<bool> RegistrarTransaccionAsync(Guid cuentaId, CrearTransaccionDTO dto)
        {
            var cuenta = await _cuentaRepo.ObtenerPorIdAsync(cuentaId);
            if (cuenta == null) throw new Exception("Cuenta no encontrada");

            if (dto.Tipo == "Egreso" && cuenta.SaldoActual < dto.Monto)
                throw new Exception("Saldo insuficiente");

            var transaccion = new TransaccionFinanciera
            {
                CuentaBancariaId = cuentaId,
                Tipo = dto.Tipo,
                Monto = dto.Monto,
                Descripcion = dto.Descripcion,
                Referencia = dto.Referencia,
                Estado = "Completada"
            };

            if (dto.Tipo == "Ingreso")
                cuenta.SaldoActual += dto.Monto;
            else
                cuenta.SaldoActual -= dto.Monto;

            await _transaccionRepo.CrearAsync(transaccion);
            await _cuentaRepo.ActualizarAsync(cuenta);

            return true;
        }
    }
}
