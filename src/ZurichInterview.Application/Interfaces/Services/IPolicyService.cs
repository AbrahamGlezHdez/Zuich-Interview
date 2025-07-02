using ZurichInterview.Application.Dtos.Client;

namespace ZurichInterview.Application.Interfaces.Services;

public interface IPolicyService
{
    Task<IEnumerable<PolicyDto>> GetAllAsync();
    Task<PolicyDto?> GetByIdAsync(int id);
    Task<List<PolicyDto>> GetByCientAsync(int id);
    Task<PolicyDto> CreateAsync(PolicyDto dto);
    Task<PolicyDto> UpdateAsync(int id, PolicyDto dto);
    Task DeleteAsync(int id);
    Task<List<PolicyDto>> GetByUsuarioIdAsync(int usuarioId);
    Task<bool> CancelByUsuarioAsync(int policyId, int usuarioId);
}