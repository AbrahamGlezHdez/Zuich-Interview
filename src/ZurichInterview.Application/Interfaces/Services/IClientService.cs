using ZurichInterview.Application.Dtos.Client;

namespace ZurichInterview.Application.Interfaces.Services;

public interface IClientService
{
    Task<ClientDto> CreateAsync(ClientDto dto);
    Task<IEnumerable<ClientDto>> GetAllAsync();
    Task<ClientDto?> GetByIdAsync(Guid id);
    Task<ClientDto> UpdateAsync(Guid id, ClientDto dto);
    Task DeleteAsync(Guid id);
}