using FluentValidation;
using NotificationMessageSender.API.DTOs.Requests.Company;

namespace NotificationMessageSender.API.Application.Validations
{
    public class CreateCompanyRequestValidation : AbstractValidator<CreateCompanyRequest>
    {
        public CreateCompanyRequestValidation()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O email não pode estar vazio.")
                .EmailAddress().WithMessage("Formato inválido");

            RuleFor(c => c.Cnpj)
                .NotEmpty().WithMessage("O cnpj não pode estar vazio.")
                .Length(14).WithMessage("O cnpj precisa ter 14 dígitos.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O nome da empresa não pode estar vazio.");

            RuleFor(c => c.Contract)
                .NotEmpty().WithMessage("A empresa precisa ter um contrato.");
        }
    }
}
