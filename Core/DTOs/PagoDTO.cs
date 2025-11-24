namespace ContabilidadSantaCruz.Core.DTOs
{
    public class PagoDTO
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string Beneficiario { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAutorizacion { get; set; }
    }

    public class CrearPagoDTO
    {
        public string Tipo { get; set; }
        public string Concepto { get; set; }
        public decimal Monto { get; set; }
        public string Beneficiario { get; set; }
        public string CuentaBancariaDestino { get; set; }
    }
}
