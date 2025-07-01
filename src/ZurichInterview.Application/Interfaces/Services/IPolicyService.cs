using ZurichInterview.Application.Dtos.Client;

namespace ZurichInterview.Application.Interfaces.Services;

public interface IPolicyService
{
    Task<IEnumerable<PolicyDto>> GetAllAsync();
    Task<PolicyDto?> GetByIdAsync(Guid id);
    Task<PolicyDto> CreateAsync(PolicyDto dto);
    Task<PolicyDto> UpdateAsync(Guid id, PolicyDto dto);
    Task DeleteAsync(Guid id);
}