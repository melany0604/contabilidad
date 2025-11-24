using ContabilidadSantaCruz.Core.DTOs;
using ContabilidadSantaCruz.Core.Entidades;
using ContabilidadSantaCruz.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContabilidadSantaCruz.Core.Servicios
{
    public interface IGestionProveedoresService
    {
        Task<ProveedorDTO> CrearProveedorAsync(CrearProveedorDTO dto);
        Task<ProveedorDTO> ObtenerProveedorAsync(Guid id);
        Task<IEnumerable<ProveedorDTO>> ObtenerTodosAsync();
    }

    public class ProveedorDTO
    {
        public Guid Id { get; set; }
        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public string Contacto { get; set; }
    }

    public class CrearProveedorDTO
    {
        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }

    public class GestionProveedoresService : IGestionProveedoresService
    {
        private readonly IProveedorRepositorio _proveedorRepo;

        public GestionProveedoresService(IProveedorRepositorio proveedorRepo)
        {
            _proveedorRepo = proveedorRepo;
        }

        public async Task<ProveedorDTO> CrearProveedorAsync(CrearProveedorDTO dto)
        {
            var proveedor = new Proveedor
            {
                Nit = dto.Nit,
                RazonSocial = dto.RazonSocial,
                Contacto = dto.Contacto,
                Telefono = dto.Telefono,
                Email = dto.Email
            };

            await _proveedorRepo.CrearAsync(proveedor);

            return new ProveedorDTO
            {
                Id = proveedor.Id,
                Nit = proveedor.Nit,
                RazonSocial = proveedor.RazonSocial,
                Contacto = proveedor.Contacto
            };
        }

        public async Task<ProveedorDTO> ObtenerProveedorAsync(Guid id)
        {
            var proveedor = await _proveedorRepo.ObtenerPorIdAsync(id);
            if (proveedor == null) throw new Exception("Proveedor no encontrado");

            return new ProveedorDTO
            {
                Id = proveedor.Id,
                Nit = proveedor.Nit,
                RazonSocial = proveedor.RazonSocial,
                Contacto = proveedor.Contacto
            };
        }

        public async Task<IEnumerable<ProveedorDTO>> ObtenerTodosAsync()
        {
            var proveedores = await _proveedorRepo.ObtenerTodosAsync();
            var resultado = new List<ProveedorDTO>();

            foreach (var prov in proveedores)
            {
                resultado.Add(new ProveedorDTO
                {
                    Id = prov.Id,
                    Nit = prov.Nit,
                    RazonSocial = prov.RazonSocial,
                    Contacto = prov.Contacto
                });
            }

            return resultado;
        }
    }
}
