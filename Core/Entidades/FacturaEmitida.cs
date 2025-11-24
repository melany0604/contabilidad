using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class FacturaEmitida
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NumeroFactura { get; set; }
        public string CiCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Emitida"; // Emitida, Pagada, Anulada
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        public DateTime FechaPago { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual ICollection<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
    }
}
