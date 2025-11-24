namespace ContabilidadSantaCruz.Core.DTOs
{
    public class CuentaBancariaDTO
    {
        public Guid Id { get; set; }
        public string NumeroCuenta { get; set; }
        public string NombreBanco { get; set; }
        public decimal SaldoActual { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }
    }

    public class CrearCuentaBancariaDTO
    {
        public string NumeroCuenta { get; set; }
        public string NombreBanco { get; set; }
        public decimal SaldoInicial { get; set; }
        public string Moneda { get; set; } = "BOB";
    }
}
