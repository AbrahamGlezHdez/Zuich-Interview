using MediatR;
using ZurichInterview.Application.Interfaces.Authentication;
using ZurichInterview.Application.Interfaces.Services;
using ZurichInterview.Domain.Entities;

namespace ZurichInterview.Application.Auth.Commands.Register;

public class RegisterCommandHandler: IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUsuarioService _usuarioService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(
        IUsuarioService usuarioService,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioService = usuarioService;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _usuarioService.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new ApplicationException("El correo ya está registrado.");

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var usuario = new Usuario
        {
            Email = request.Email,
            Password = hashedPassword,
            Rol = request.Rol
        };

        await _usuarioService.AddAsync(usuario);

        var token = _jwtTokenGenerator.GenerateToken(usuario.Id.ToString(), usuario.Email, usuario.Rol);

        return new RegisterResponse
        {
            Id = usuario.Id.ToString(),
            Email = usuario.Email,
            Rol = usuario.Rol,
            Token = token
        };
    }
}