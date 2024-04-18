using FluentValidation;
using NotificationMessageSender.API.DTOs.Requests.User;

namespace NotificationMessageSender.API.Application.Validations
{
    public class CreateUserRequestValidations : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidations()
        {
            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage("A senha não pode estar vazia.")
                .MinimumLength(8)
                .WithMessage("A senha deve ter no mínimo 8 caracteres.")
                .Matches("[a-z]")
                .WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches("[A-Z]")
                .WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches("[0-9]")
                .WithMessage("A senha deve conter pelo menos um número.")
                .Matches("[^\\s]")
                .WithMessage("A senha não pode conter espaços em branco.");

            RuleFor(c => c.Cpf).NotEmpty().Length(11).WithMessage("O cpf precisa conter 11 dígitos.");
            RuleFor(c => c.Name).NotEmpty().WithMessage("O nome não pode estar vazio.");
            RuleFor(c => c.CompanyId).NotEmpty().WithMessage("O usuário precisa estar vinculado a uma empresa.");
        }
    }
}
