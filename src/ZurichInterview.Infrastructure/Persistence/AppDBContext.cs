using Microsoft.EntityFrameworkCore;
using ZurichInterview.Domain.Entities;
using ZurichInterview.Domain.Entities.Enums;

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
        InitialInformation(modelBuilder);
        
        
    }
    
    private void InitialInformation(ModelBuilder modelBuilder)
    {
        // Usuarios
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Email = "admin@example.com",
                Password = "$2a$11$eaLolK4l0m4DFjD8icYqUOYZ0MWtXs7npX6Cdxto9TJHO5Amku1Ia", // Reemplaza por hash real
                Rol = "Administrador"
            },
            new Usuario
            {
                Id = 2,
                Email = "client@example.com",
                Password = "$2a$11$eaLolK4l0m4DFjD8icYqUOYZ0MWtXs7npX6Cdxto9TJHO5Amku1Ia", // Reemplaza por hash real
                Rol = "Cliente"
            }
        );
        
        // Cliente
        modelBuilder.Entity<Client>().HasData(
            new Client
            {
                Id = 1,
                IdentificationNumber = "1234567890",
                Name = "Juan",
                MiddleName = "Carlos",
                SurName = "Pérez",
                Email = "client@example.com",
                Phone = "123456789",
                Address = "Av. Siempre Viva 123",
                UsuarioId = 2
            }
        );
        
        // Pólizas
        modelBuilder.Entity<Policy>().HasData(
            new Policy
            {
                Id = 1,
                ClientId = 1,
                Type = PolicyType.Life,
                StartDate = new DateTime(2025, 7, 1),
                ExpirationDate = new DateTime(2026, 7, 1),
                Amount = 10000,
                Status = PolicyStatus.Active
            },
            new Policy
            {
                Id = 2,
                ClientId = 1,
                Type = PolicyType.Health,
                StartDate = new DateTime(2025, 7, 1),
                ExpirationDate = new DateTime(2026, 7, 1),
                Amount = 8000,
                Status = PolicyStatus.Cancelled
            },
            new Policy
            {
                Id = 3,
                ClientId = 1,
                Type = PolicyType.Car,
                StartDate = new DateTime(2025, 7, 1),
                ExpirationDate = new DateTime(2026, 7, 1),
                Amount = 15000,
                Status = PolicyStatus.Active
            }
        );
    }
    
    
}