using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Core.Common.Exceptions;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.MessageBus.Services.Interfaces;
using NotificationMessageSender.Infraestructure.Repositories;
using System.Text.RegularExpressions;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Notification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationResponse>
    {
        IMessageBus _messageBus;
        IUserRepository _userRepository;
        ICompanyRepository _companyRepository;
        INotificationRepository _notificationRepository;

        public CreateNotificationCommandHandler(IMessageBus messageBus, IUserRepository userRepository, 
            ICompanyRepository companyRepository, INotificationRepository notificationRepository)
        {
            _messageBus = messageBus;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _notificationRepository = notificationRepository;
        }

        public Task<CreateNotificationResponse> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.Get(command.UserSender, "Users", cancellationToken).Result;
            var company = _companyRepository.Get(user.CompanyId, "Companies", cancellationToken).Result;

            if (!NaoChegouNoLimiteDoContrato(company, cancellationToken))
                throw new LimiteDeNotificacoesAtingidaException();

            if (!IsValidEmail(command.Receiver)) throw new Exception("Email de destino inválido.");



            command.Id = Guid.NewGuid();
            _messageBus.Publish("notifications", "send-notification-" + command.Type, command);
            return Task.FromResult(new CreateNotificationResponse(command.Id, DateTime.Now.ToUniversalTime()));
        }

        private bool NaoChegouNoLimiteDoContrato(CompanyEntity company, CancellationToken cancellationToken)
        {
            var notificationsRequests = _notificationRepository.GetAllRequestsOfDayByCompany(DateOnly.FromDateTime(DateTime.Today), company.Id, cancellationToken).Result;

            Dictionary<ContractEnum, int> limitesPorContrato = new Dictionary<ContractEnum, int>
            {
                { ContractEnum.Prata, 5 },
                { ContractEnum.Ouro, 15 },
                { ContractEnum.Diamante, 100 }
            };

            return notificationsRequests.Count < limitesPorContrato.GetValueOrDefault(company.Contract);
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([a-z\d]+(-[a-z\d]+)*\.)+[a-z]{2,}$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

    }
}
