using ZurichInterview.Domain.Entities;

namespace ZurichInterview.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<Usuario?> GetByEmailAsync(string email);
    Task AddAsync(Usuario usuario);
}