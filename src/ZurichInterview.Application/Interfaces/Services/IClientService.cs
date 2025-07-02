using ZurichInterview.Application.Dtos.Client;

namespace ZurichInterview.Application.Interfaces.Services;

public interface IClientService
{
    Task<ClientDto> CreateAsync(ClientDto dto);
    Task<IEnumerable<ClientDto>> GetAllAsync();
    Task<ClientDto?> GetByIdAsync(int id);
    Task<ClientDto> UpdateAsync(int id, ClientDto dto);
    Task<ClientDto?> GetByUsuarioIdAsync(int usuarioId);
    Task DeleteAsync(int id);
}