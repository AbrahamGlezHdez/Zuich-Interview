using ZurichInterview.Domain.Entities.Enums;
using ZurichInterview.Infrastructure.Persistence;
using ZurichInterview.Infrastructure.Services;

namespace ZurichInterview.UnitTests;

public partial class PolicyServiceGetTests
{
    [Fact]
    public async Task GetByUsuarioIdAsync_ReturnsPoliciesForUsuario()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetByUsuarioIdAsync(10);

        Assert.NotNull(result);
        Assert.All(result, p => Assert.Equal(10, context.Clients.First(c => c.Id == p.ClientId).UsuarioId));
    }

    [Fact]
    public async Task GetByUsuarioIdAsync_ReturnsEmptyList_WhenNoPolicies()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        var result = await service.GetByUsuarioIdAsync(999);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task CancelByUsuarioAsync_CancelsPolicy_WhenValid()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var result = await service.CancelByUsuarioAsync(1, 10);

        Assert.True(result);

        var policy = await context.Policies.FindAsync(1);
        Assert.Equal(PolicyStatus.Cancelled, policy.Status);
    }

    [Fact]
    public async Task CancelByUsuarioAsync_ReturnsFalse_WhenPolicyNotFound()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        var result = await service.CancelByUsuarioAsync(999, 10);

        Assert.False(result);
    }

    [Fact]
    public async Task CancelByUsuarioAsync_ReturnsFalse_WhenAlreadyCancelled()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var result = await service.CancelByUsuarioAsync(2, 10);

        Assert.False(result);
    }
}