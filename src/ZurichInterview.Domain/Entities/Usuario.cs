using ZurichInterview.Domain.Constants;

namespace ZurichInterview.Domain.Entities;

public class Usuario
{
    public int Id { get; set; } = 0;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Rol { get; set; } = Roles.Cliente; // o "Administrador"
}