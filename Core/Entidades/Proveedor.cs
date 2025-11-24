using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class Proveedor
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nit { get; set; }
        public string RazonSocial { get; set; }
        public string Contacto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Estado { get; set; } = "Activo";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual ICollection<CuentaPorPagar> CuentasPorPagar { get; set; } = new List<CuentaPorPagar>();
    }
}
