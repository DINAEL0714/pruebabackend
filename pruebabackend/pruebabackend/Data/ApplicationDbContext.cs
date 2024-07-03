using Microsoft.EntityFrameworkCore;
using pruebabackend.Models;

namespace pruebabackend.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Rent> Rents { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasKey(e => e.Plate);

                entity.Property(e => e.Plate).HasMaxLength(10);

                entity.Property(e => e.Brand)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Available).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Idclient);

                entity.Property(e => e.Idcard)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone1).HasMaxLength(15);

                entity.Property(e => e.Phone2).HasMaxLength(15);
            });

            modelBuilder.Entity<Rent>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TotalValue).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.InitialPayment).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Returned).HasDefaultValueSql("((1))");

                entity.Property(e => e.RegisterState).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.IdplateNavigation)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.Idplate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rent_car");

                entity.HasOne(d => d.IdclientNavigation)
                    .WithMany(p => p.Rents)
                    .HasForeignKey(d => d.Idclient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rent_client");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Value).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdrentNavigation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.Idrent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payments_rent");
            });
        }
    }
}
