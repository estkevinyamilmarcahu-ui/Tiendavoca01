using Microsoft.EntityFrameworkCore;
using FerreteriaValdivia.Models;

namespace FerreteriaValdivia.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Producto (tabla y columnas consistentes con las migraciones)
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(p => p.CodigoProducto);

                entity.Property(p => p.CodigoProducto)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(p => p.Nombre)
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(p => p.PrecioVenta)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();

                entity.Property(p => p.StockMinimo)
                    .IsRequired();

                entity.Property(p => p.StockActual)
                    .IsRequired();

                entity.Property(p => p.CantidadVendida)
                    .HasDefaultValue(0);
            });

            // Configuración de Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.IdUsuario);
            });
        }
    }
}
