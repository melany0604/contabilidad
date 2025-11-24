using System;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class TransaccionFinanciera
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CuentaBancariaId { get; set; }
        public string Tipo { get; set; } // "Ingreso" o "Egreso"
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; } // Número de factura, cheque, etc.
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Completada, Rechazada
        public DateTime FechaOperacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Foreign Key
        public virtual CuentaBancaria CuentaBancaria { get; set; }
    }
}
