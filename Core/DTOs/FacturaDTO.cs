namespace ContabilidadSantaCruz.Core.DTOs
{
    public class FacturaDTO
    {
        public Guid Id { get; set; }
        public string NumeroFactura { get; set; }
        public string CiCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public DateTime FechaEmision { get; set; }
    }

    public class CrearFacturaDTO
    {
        public string CiCliente { get; set; }
        public string NombreCliente { get; set; }
        public List<DetalleFacturaDTO> Detalles { get; set; }
    }

    public class DetalleFacturaDTO
    {
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
