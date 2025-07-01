using MediatR;

namespace ZurichInterview.Application.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Rol { get; set; } = default!;
}