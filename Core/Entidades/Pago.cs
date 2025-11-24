using System;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class Pago
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Tipo { get; set; } // "Proveedor", "Nómina", "Factura"
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string Beneficiario { get; set; }
        public string CuentaBancariaDestino { get; set; }
        public string Estado { get; set; } = "Autorizado"; // Autorizado, Ejecutado, Cancelado
        public string NumeroTransferencia { get; set; }
        public DateTime FechaAutorizacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaEjecucion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
