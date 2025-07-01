using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Entities;
using ZurichInterview.Infrastructure.Persistence;

namespace ZurichInterview.Infrastructure.Services;

public class PolicyService : IPolicyService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public PolicyService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PolicyDto>> GetAllAsync()
    {
        var policies = await _context.Policies.ToListAsync();
        return _mapper.Map<IEnumerable<PolicyDto>>(policies);
    }

    public async Task<PolicyDto?> GetByIdAsync(Guid id)
    {
        var policy = await _context.Policies.FindAsync(id);
        return policy == null ? null : _mapper.Map<PolicyDto>(policy);
    }

    public async Task<PolicyDto> CreateAsync(PolicyDto dto)
    {
        var policy = _mapper.Map<Policy>(dto);
        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();
        return _mapper.Map<PolicyDto>(policy);
    }

    public async Task<PolicyDto> UpdateAsync(Guid id, PolicyDto dto)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
            throw new KeyNotFoundException("Póliza no encontrada");

        _mapper.Map(dto, policy);
        await _context.SaveChangesAsync();
        return _mapper.Map<PolicyDto>(policy);
    }

    public async Task DeleteAsync(Guid id)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy != null)
        {
            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
        }
    }
}