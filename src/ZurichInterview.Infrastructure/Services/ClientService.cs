using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ZurichInterview.Application.Dtos.Client;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Entities;
using ZurichInterview.Infrastructure.Persistence;

namespace ZurichInterview.Infrastructure.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public ClientService(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ClientDto> CreateAsync(ClientDto dto)
    {
        var entity = _mapper.Map<Client>(dto);
        entity.Id = Guid.NewGuid();

        _db.Clients.Add(entity);
        await _db.SaveChangesAsync();

        return _mapper.Map<ClientDto>(entity);
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await _db.Clients.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<ClientDto>>(clients);
    }

    public async Task<ClientDto?> GetByIdAsync(Guid id)
    {
        var client = await _db.Clients.FindAsync(id);
        return client == null ? null : _mapper.Map<ClientDto>(client);
    }

    public async Task<ClientDto> UpdateAsync(Guid id, ClientDto dto)
    {
        var client = await _db.Clients.FindAsync(id);
        if (client == null) throw new Exception("Cliente no encontrado");

        _mapper.Map(dto, client);
        await _db.SaveChangesAsync();

        return _mapper.Map<ClientDto>(client);
    }

    public async Task DeleteAsync(Guid id)
    {
        var client = await _db.Clients.FindAsync(id);
        if (client == null) throw new Exception("Cliente no encontrado");

        _db.Clients.Remove(client);
        await _db.SaveChangesAsync();
    }
}