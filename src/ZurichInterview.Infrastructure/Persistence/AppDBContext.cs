using Microsoft.EntityFrameworkCore;
using ZurichInterview.Domain.Entities;

namespace ZurichInterview.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Policy> Policies => Set<Policy>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.IdentificationNumber).IsUnique();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            entity.Property(c => c.MiddleName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.SurName).IsRequired().HasMaxLength(100);
            entity.Property(c => c.Email).IsRequired();
            entity.Property(c => c.Phone).IsRequired();
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
            entity.HasOne(p => p.Client)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.ClientId);
        });
    }
}