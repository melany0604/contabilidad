namespace ContabilidadSantaCruz.Core.DTOs
{
    public class PresupuestoDTO
    {
        public Guid Id { get; set; }
        public string NombreCentro { get; set; }
        public string Periodo { get; set; }
        public decimal MontoAsignado { get; set; }
        public decimal MontoGastado { get; set; }
        public decimal MontoDisponible { get; set; }
        public string Estado { get; set; }
    }

    public class CrearPresupuestoDTO
    {
        public string NombreCentro { get; set; }
        public string Periodo { get; set; }
        public decimal MontoAsignado { get; set; }
    }

    public class ActualizarPresupuestoDTO
    {
        public decimal MontoGastado { get; set; }
    }
}
