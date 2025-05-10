using Microsoft.EntityFrameworkCore;
using CQRSDDDDataRest.Domain.Entities;

namespace CQRSDDDDataRest.Infrastructure.Persistence
{
    public class PostgresPagosDbContext : DbContext
    {
        public PostgresPagosDbContext(DbContextOptions<PostgresPagosDbContext> options) : base(options) { }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>(builder =>
            {
                builder.ToTable("pagos");

                builder.HasKey(p => p.Id);

                builder.Property(p => p.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd(); // Para auto-incremental

                builder.Property(p => p.ClienteId)
                    .HasColumnName("clienteid")
                    .HasMaxLength(25)
                    .IsRequired();

                builder.Property(p => p.FechaPago)
                    .HasColumnName("fechapago")
                    .IsRequired();

                builder.Property(p => p.Estado)
                    .HasColumnName("estado")
                    .HasMaxLength(20);

                builder.OwnsOne(p => p.Monto, montoBuilder =>
                {
                    montoBuilder.Property(m => m.Valor)
                        .HasColumnName("monto")
                        .HasColumnType("money") // Tipo MONEY en PostgreSQL
                        .IsRequired();
                });

                builder.OwnsOne(p => p.MetodoPago, metodoPagoBuilder =>
                {
                    metodoPagoBuilder.Property(m => m.Valor)
                        .HasColumnName("metodopago")
                        .HasMaxLength(50);
                });
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}