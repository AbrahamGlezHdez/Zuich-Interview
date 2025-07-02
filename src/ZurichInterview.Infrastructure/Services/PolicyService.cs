using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Entities;
using ZurichInterview.Domain.Entities.Enums;
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

    public async Task<PolicyDto?> GetByIdAsync(int id)
    {
        var policy = await _context.Policies.FindAsync(id);
        return policy == null ? null : _mapper.Map<PolicyDto>(policy);
    }
    
    public async Task<List<PolicyDto>> GetByCientAsync(int clientId)
    {
        var policies = await _context.Policies
            .Where(p => p.ClientId == clientId)
            .ToListAsync();

        return _mapper.Map<List<PolicyDto>>(policies);
    }

    public async Task<PolicyDto> CreateAsync(PolicyDto dto)
    {
        var policy = _mapper.Map<Policy>(dto);
        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();
        return _mapper.Map<PolicyDto>(policy);
    }

    public async Task<PolicyDto> UpdateAsync(int id, PolicyDto dto)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
            throw new KeyNotFoundException("Póliza no encontrada");

        _mapper.Map(dto, policy);
        await _context.SaveChangesAsync();
        return _mapper.Map<PolicyDto>(policy);
    }

    public async Task DeleteAsync(int id)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy != null)
        {
            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<List<PolicyDto>> GetByUsuarioIdAsync(int usuarioId)
    {
        var policies = await _context.Policies
            .Include(p => p.Client)
            .Where(p => p.Client.UsuarioId == usuarioId)
            .ToListAsync();

        return _mapper.Map<List<PolicyDto>>(policies);
    }

    public async Task<bool> CancelByUsuarioAsync(int policyId, int usuarioId)
    {
        var policy = await _context.Policies
            .Include(p => p.Client)
            .FirstOrDefaultAsync(p => p.Id == policyId && p.Client.UsuarioId == usuarioId);

        if (policy == null || policy.Status == PolicyStatus.Cancelled)
            return false;

        policy.Status = PolicyStatus.Cancelled;
        await _context.SaveChangesAsync();

        return true;
    }
}