using Microsoft.EntityFrameworkCore;
using ZurichInterview.Domain.Entities;

namespace ZurichInterview.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Policy> Policies => Set<Policy>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

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
            entity.HasOne(c => c.Usuario)
                .WithOne()
                .HasForeignKey<Client>(c => c.UsuarioId)
                .IsRequired(false);
        });

        modelBuilder.Entity<Policy>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
            entity.HasOne(p => p.Client)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.ClientId);
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
    
            entity.Property(p => p.Status)
                .HasConversion<string>() // <--- aquí la magia
                .IsRequired();

            entity.HasOne(p => p.Client)
                .WithMany(c => c.Policies)
                .HasForeignKey(p => p.ClientId);
        });
        
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.Password)
                .IsRequired();

            entity.Property(u => u.Rol)
                .IsRequired()
                .HasMaxLength(20);
        });
        
    }
}