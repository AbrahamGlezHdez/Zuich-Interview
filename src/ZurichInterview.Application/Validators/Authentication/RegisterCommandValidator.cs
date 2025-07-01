using FluentValidation;
using ZurichInterview.Application.Auth.Commands.Register;
using ZurichInterview.Domain.Constants;

namespace ZurichInterview.Application.Validators.Authentication;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo es obligatorio.")
            .EmailAddress().WithMessage("Debe ser un correo válido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

        RuleFor(x => x.Rol)
            .NotEmpty().WithMessage("El rol es obligatorio.")
            .Must(rol => rol == Roles.Administrador || rol == Roles.Cliente)
            .WithMessage("El rol debe ser 'Administrador' o 'Cliente'.");
    }
}