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
    [Fact]
    public async Task CreateAsync_CreatesAndReturnsPolicy()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        var dto = new PolicyDto
        {
            ClientId = 1,
            Type = PolicyType.Car,
            StartDate = DateTime.Today,
            ExpirationDate = DateTime.Today.AddYears(1),
            Amount = 1500,
            Status = PolicyStatus.Active
        };

        var created = await service.CreateAsync(dto);

        Assert.NotNull(created);
        Assert.Equal(dto.ClientId, created.ClientId);
        Assert.True(created.Id > 0);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesPolicy_WhenExists()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        var updateDto = new PolicyDto
        {
            Id = 1,
            ClientId = 1,
            Type = PolicyType.Health,
            StartDate = DateTime.Today,
            ExpirationDate = DateTime.Today.AddYears(2),
            Amount = 3000,
            Status = PolicyStatus.Active
        };

        var updated = await service.UpdateAsync(1, updateDto);

        Assert.NotNull(updated);
        Assert.Equal(3000, updated.Amount);
        Assert.Equal(PolicyType.Health, updated.Type);
    }

    [Fact]
    public async Task UpdateAsync_Throws_WhenPolicyNotFound()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        var updateDto = new PolicyDto
        {
            ClientId = 1,
            Type = PolicyType.Car,
            StartDate = DateTime.Today,
            ExpirationDate = DateTime.Today.AddYears(1),
            Amount = 1000,
            Status = PolicyStatus.Active
        };

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.UpdateAsync(999, updateDto));
    }

    [Fact]
    public async Task DeleteAsync_DeletesPolicy_WhenExists()
    {
        using var context = new AppDbContext(_dbOptions);
        await SeedData(context);
        var service = new PolicyService(context, _mapper);

        await service.DeleteAsync(1);

        var deleted = await context.Policies.FindAsync(1);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task DeleteAsync_DoesNothing_WhenPolicyNotExists()
    {
        using var context = new AppDbContext(_dbOptions);
        var service = new PolicyService(context, _mapper);

        await service.DeleteAsync(999);
    }
}
