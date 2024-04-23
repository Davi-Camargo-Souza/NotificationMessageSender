using FluentValidation;
using NotificationMessageSender.API.Application.CQRS.Commands.Company;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;

namespace NotificationMessageSender.API.Application.Validations
{
    public class CreateNotificationCommandValidations : AbstractValidator<CreateNotificationCommand>
    {
        public CreateNotificationCommandValidations()
        {
            RuleFor(u => u.Message).NotEmpty().WithMessage("A mensagem não pode estar vazia.");
            RuleFor(u => u.Receiver).NotEmpty().WithMessage("O destinatário não pode estar vazio.");
            RuleFor(u => u.Subject).NotEmpty().WithMessage("O assunto não pode estar vazio.");
        }
    }
}
