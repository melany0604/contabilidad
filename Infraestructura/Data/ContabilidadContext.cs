using ContabilidadSantaCruz.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadSantaCruz.Infraestructura.Data
{
    public class ContabilidadContext : DbContext
    {
        public ContabilidadContext(DbContextOptions<ContabilidadContext> options) : base(options)
        {
        }

        public DbSet<CuentaBancaria> CuentasBancarias { get; set; }
        public DbSet<TransaccionFinanciera> TransaccionesFinancieras { get; set; }
        public DbSet<FacturaEmitida> FacturasEmitidas { get; set; }
        public DbSet<DetalleFactura> DetallesFacturas { get; set; }
        public DbSet<PresupuestoCentro> Presupuestos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<CuentaPorPagar> CuentasPorPagar { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CuentaBancaria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroCuenta).IsRequired().HasMaxLength(20);
                entity.Property(e => e.NombreBanco).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SaldoActual).HasColumnType("numeric(18,2)");
                entity.HasIndex(e => e.NumeroCuenta).IsUnique();
            });

            modelBuilder.Entity<TransaccionFinanciera>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).HasColumnType("numeric(18,2)");
                entity.HasOne(e => e.CuentaBancaria)
                    .WithMany(c => c.Transacciones)
                    .HasForeignKey(e => e.CuentaBancariaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FacturaEmitida>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NumeroFactura).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Subtotal).HasColumnType("numeric(18,2)");
                entity.Property(e => e.Iva).HasColumnType("numeric(18,2)");
                entity.Property(e => e.Total).HasColumnType("numeric(18,2)");
                entity.HasIndex(e => e.NumeroFactura).IsUnique();
            });

            modelBuilder.Entity<DetalleFactura>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PrecioUnitario).HasColumnType("numeric(18,2)");
                entity.Property(e => e.Subtotal).HasColumnType("numeric(18,2)");
                entity.HasOne(e => e.Factura)
                    .WithMany(f => f.Detalles)
                    .HasForeignKey(e => e.FacturaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PresupuestoCentro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MontoAsignado).HasColumnType("numeric(18,2)");
                entity.Property(e => e.MontoGastado).HasColumnType("numeric(18,2)");
                entity.Property(e => e.MontoDisponible).HasColumnType("numeric(18,2)");
                entity.HasIndex(e => new { e.NombreCentro, e.Periodo }).IsUnique();
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).HasColumnType("numeric(18,2)");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nit).IsRequired().HasMaxLength(15);
                entity.HasIndex(e => e.Nit).IsUnique();
            });

            modelBuilder.Entity<CuentaPorPagar>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Monto).HasColumnType("numeric(18,2)");
                entity.HasOne(e => e.Proveedor)
                    .WithMany(p => p.CuentasPorPagar)
                    .HasForeignKey(e => e.ProveedorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
