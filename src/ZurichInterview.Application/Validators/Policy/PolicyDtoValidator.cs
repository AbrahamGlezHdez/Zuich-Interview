using FluentValidation;
using ZurichInterview.Application.Dtos.Client;

namespace ZurichInterview.Application.Validators.Policy;

public class PolicyDtoValidator : AbstractValidator<PolicyDto>
{
    public PolicyDtoValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El cliente es obligatorio");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.ExpirationDate)
            .WithMessage("La fecha de inicio debe ser anterior a la de expiración");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("El monto asegurado debe ser mayor a 0");
    }
}