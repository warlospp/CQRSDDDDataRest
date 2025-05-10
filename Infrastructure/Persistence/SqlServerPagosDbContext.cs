using Microsoft.EntityFrameworkCore;
using CQRSDDDDataRest.Domain.Entities;

namespace CQRSDDDDataRest.Infrastructure.Persistence
{
    public class SqlServerPagosDbContext : DbContext
    {
        public SqlServerPagosDbContext(DbContextOptions<SqlServerPagosDbContext> options) : base(options) { }

        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>(builder =>
            {
                builder.OwnsOne(p => p.Monto, o =>
                {
                    o.Property(m => m.Valor).HasColumnName("Monto");
                });
                builder.OwnsOne(p => p.MetodoPago, o =>
                {
                    o.Property(m => m.Valor).HasColumnName("MetodoPago");
                });
                builder.HasKey(p => p.Id);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}