using FluentValidation;
using ZurichInterview.Application.Dtos.Client;

namespace ZurichInterview.Application.Validators.Client;

public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    public ClientDtoValidator()
    {
        RuleFor(x => x.IdentificationNumber)
            .NotEmpty().WithMessage("El número de identificación es obligatorio")
            .Length(10).WithMessage("Debe tener 10 dígitos")
            .Matches("^[0-9]+$").WithMessage("Solo debe contener números");

        RuleForEach(x => new[] { x.Name, x.MiddleName, x.SurName })
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑ\\s]+$").WithMessage("No debe contener números ni caracteres especiales");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Correo obligatorio")
            .EmailAddress().WithMessage("Correo inválido");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Teléfono obligatorio");
    }
}