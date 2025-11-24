using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Servicios
{
    public interface IGestionFacturasService
    {
        Task<FacturaDTO> EmitirFacturaAsync(CrearFacturaDTO dto);
        Task<FacturaDTO> ObtenerFacturaAsync(Guid id);
        Task<IEnumerable<FacturaDTO>> ObtenerFacturasClienteAsync(string ci);
        Task<IEnumerable<FacturaDTO>> ObtenerTodasAsync();
        Task<bool> RegistrarPagoFacturaAsync(Guid facturaId);
    }

    public class GestionFacturasService : IGestionFacturasService
    {
        private readonly IFacturaRepositorio _facturaRepo;

        public GestionFacturasService(IFacturaRepositorio facturaRepo)
        {
            _facturaRepo = facturaRepo;
        }

        public async Task<FacturaDTO> EmitirFacturaAsync(CrearFacturaDTO dto)
        {
            decimal subtotal = 0;
            var detalles = new List<DetalleFactura>();

            foreach (var detalle in dto.Detalles)
            {
                decimal subDetalle = detalle.Cantidad * detalle.PrecioUnitario;
                subtotal += subDetalle;

                detalles.Add(new DetalleFactura
                {
                    Descripcion = detalle.Descripcion,
                    Cantidad = detalle.Cantidad,
                    PrecioUnitario = detalle.PrecioUnitario,
                    Subtotal = subDetalle
                });
            }

            decimal iva = subtotal * 0.13m; // 13% IVA
            decimal total = subtotal + iva;

            var factura = new FacturaEmitida
            {
                NumeroFactura = GenerarNumeroFactura(),
                CiCliente = dto.CiCliente,
                NombreCliente = dto.NombreCliente,
                Subtotal = subtotal,
                Iva = iva,
                Total = total,
                Detalles = detalles
            };

            await _facturaRepo.CrearAsync(factura);

            return new FacturaDTO
            {
                Id = factura.Id,
                NumeroFactura = factura.NumeroFactura,
                CiCliente = factura.CiCliente,
                NombreCliente = factura.NombreCliente,
                Total = factura.Total,
                Estado = factura.Estado,
                FechaEmision = factura.FechaEmision
            };
        }

        public async Task<FacturaDTO> ObtenerFacturaAsync(Guid id)
        {
            var factura = await _facturaRepo.ObtenerPorIdAsync(id);
            if (factura == null) throw new Exception("Factura no encontrada");

            return new FacturaDTO
            {
                Id = factura.Id,
                NumeroFactura = factura.NumeroFactura,
                CiCliente = factura.CiCliente,
                NombreCliente = factura.NombreCliente,
                Total = factura.Total,
                Estado = factura.Estado,
                FechaEmision = factura.FechaEmision
            };
        }

        public async Task<IEnumerable<FacturaDTO>> ObtenerFacturasClienteAsync(string ci)
        {
            var facturas = await _facturaRepo.ObtenerPorClienteAsync(ci);
            var resultado = new List<FacturaDTO>();

            foreach (var factura in facturas)
            {
                resultado.Add(new FacturaDTO
                {
                    Id = factura.Id,
                    NumeroFactura = factura.NumeroFactura,
                    CiCliente = factura.CiCliente,
                    NombreCliente = factura.NombreCliente,
                    Total = factura.Total,
                    Estado = factura.Estado,
                    FechaEmision = factura.FechaEmision
                });
            }

            return resultado;
        }

        public async Task<IEnumerable<FacturaDTO>> ObtenerTodasAsync()
        {
            var facturas = await _facturaRepo.ObtenerTodosAsync();
            var resultado = new List<FacturaDTO>();

            foreach (var factura in facturas)
            {
                resultado.Add(new FacturaDTO
                {
                    Id = factura.Id,
                    NumeroFactura = factura.NumeroFactura,
                    CiCliente = factura.CiCliente,
                    NombreCliente = factura.NombreCliente,
                    Total = factura.Total,
                    Estado = factura.Estado,
                    FechaEmision = factura.FechaEmision
                });
            }

            return resultado;
        }

        public async Task<bool> RegistrarPagoFacturaAsync(Guid facturaId)
        {
            var factura = await _facturaRepo.ObtenerPorIdAsync(facturaId);
            if (factura == null) throw new Exception("Factura no encontrada");

            factura.Estado = "Pagada";
            factura.FechaPago = DateTime.UtcNow;
            await _facturaRepo.ActualizarAsync(factura);

            return true;
        }

        private string GenerarNumeroFactura()
        {
            return $"FCT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
        }
    }
}
