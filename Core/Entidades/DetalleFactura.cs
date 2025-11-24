using System;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class DetalleFactura
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FacturaId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Foreign Key
        public virtual FacturaEmitida Factura { get; set; }
    }
}
