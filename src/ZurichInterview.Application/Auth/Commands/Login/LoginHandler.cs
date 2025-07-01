using MediatR;
using ZurichInterview.Application.Interfaces.Authentication;
using ZurichInterview.Application.Interfaces.Services;

namespace ZurichInterview.Application.Auth.Commands.Login;

public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUsuarioService _usuarioService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public LoginHandler(
        IUsuarioService usuarioService,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _usuarioService = usuarioService;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioService.GetByEmailAsync(request.Email);

        if (usuario == null)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        if (!_passwordHasher.Verify(request.Password, usuario.Password))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var token = _jwtTokenGenerator.GenerateToken(
            usuario.Id.ToString(),
            usuario.Email,
            usuario.Rol
        );

        return new LoginResponse { Token = token };
    }
}