using LabCoreSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabCoreSoft.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.DocumentType).IsRequired().HasConversion<string>();
                entity.Property(p => p.DocumentNumber).IsRequired().HasMaxLength(50);
                entity.Property(p => p.BirthDate).IsRequired();
                entity.Property(p => p.City).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Phone).HasMaxLength(20);
                entity.Property(p => p.Email).HasMaxLength(100);
                entity.Property(p => p.IsActive).IsRequired().HasDefaultValue(true);

                entity.HasIndex(p => new { p.DocumentType, p.DocumentNumber }).IsUnique();
            });
        }
    }
}