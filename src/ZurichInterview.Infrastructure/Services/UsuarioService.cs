using Microsoft.EntityFrameworkCore;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Entities;
using ZurichInterview.Infrastructure.Persistence;

namespace ZurichInterview.Infrastructure.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }
}