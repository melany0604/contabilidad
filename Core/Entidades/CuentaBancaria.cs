using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class CuentaBancaria
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NumeroCuenta { get; set; }
        public string NombreBanco { get; set; }
        public decimal SaldoActual { get; set; }
        public string Moneda { get; set; } = "BOB"; // Bolivianos
        public string Estado { get; set; } = "Activa";
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;

        // Relaciones
        public virtual ICollection<TransaccionFinanciera> Transacciones { get; set; } = new List<TransaccionFinanciera>();
    }
}
