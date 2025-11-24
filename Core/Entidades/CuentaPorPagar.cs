using System;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class CuentaPorPagar
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProveedorId { get; set; }
        public string NumeroFacturaProveedor { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Pagada, Cancelada
        public DateTime FechaRecepcion { get; set; } = DateTime.UtcNow;
        public DateTime FechaPago { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public virtual Proveedor Proveedor { get; set; }
    }
}
