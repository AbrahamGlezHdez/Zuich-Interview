namespace ZurichInterview.Application.Auth.Commands.Register;

public class RegisterResponse
{
    public string Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Rol { get; set; } = default!;
    public string Token { get; set; } = default!;
}