using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Domain.Entities;
using ZurichInterview.Domain.Entities.Enums;
using ZurichInterview.Infrastructure.Persistence;
using ZurichInterview.Infrastructure.Services;

namespace ZurichInterview.UnitTests;

public partial class PolicyServiceGetTests
{
    private readonly IMapper _mapper;
    private readonly DbContextOptions<AppDbContext> _dbOptions;

    public PolicyServiceGetTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Policy, PolicyDto>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _dbOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    private async Task SeedData(AppDbContext context)
    {
        var client = new Client
        {
            Id = 1,
            IdentificationNumber = "1234567890",
            Name = "Juan",
            MiddleName = "Carlos",
            SurName = "Pérez",
            Email = "juan.perez@email.com",
            Phone = "555-1234",
            Address = "Av. Reforma 123",
            UsuarioId = 10
        };

        context.Clients.Add(client);

        var policies = new List<Policy>
        {
            new Policy { Id = 1, ClientId = client.Id, Type = PolicyType.Car, StartDate = DateTime.Today.AddDays(-10), ExpirationDate = DateTime.Today.AddDays(10), Amount = 1000m, Status = PolicyStatus.Active, Client = client },
            new Policy { Id = 2, ClientId = client.Id, Type = PolicyType.Home, StartDate = DateTime.Today.AddDays(-20), ExpirationDate = DateTime.Today.AddDays(-5), Amount = 2000m, Status = PolicyStatus.Cancelled, Client = client }
        };

        context.Policies.AddRange(policies);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPolicies()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Contains(result, p => p.Status == PolicyStatus.Cancelled);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsPolicy_WhenExists()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotExists()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetByCientAsync_ReturnsPoliciesForClient()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetByCientAsync(1);

        Assert.NotNull(result);
        Assert.All(result, p => Assert.Equal(1, p.ClientId));
    }

    [Fact]
    public async Task GetByCientAsync_ReturnsEmptyList_WhenNoPolicies()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetByCientAsync(999);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
