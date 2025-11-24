using System;
using System.ComponentModel.DataAnnotations;

namespace ContabilidadSantaCruz.Core.Entidades
{
    public class PresupuestoCentro
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NombreCentro { get; set; } // RRHH, Marketing, Veterinaria, etc.
        public string Periodo { get; set; } // 2024-01, 2024-02, etc.
        public decimal MontoAsignado { get; set; }
        public decimal MontoGastado { get; set; }
        public decimal MontoDisponible { get; set; }
        public string Estado { get; set; } = "Vigente"; // Vigente, Cerrado
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }
}
