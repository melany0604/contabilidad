namespace ContabilidadSantaCruz.Core.DTOs
{
    public class TransaccionDTO
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
        public string Estado { get; set; }
        public DateTime FechaOperacion { get; set; }
    }

    public class CrearTransaccionDTO
    {
        public string Tipo { get; set; } // "Ingreso" o "Egreso"
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public string Referencia { get; set; }
    }
}
