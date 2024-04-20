using MediatR;
using Microsoft.AspNetCore.Http;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Core.Common.Exceptions;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Uteis;
using NotificationMessageSender.Core.MessageBus.Services.Interfaces;
using NotificationMessageSender.Infraestructure.Repositories;
using System.Text.RegularExpressions;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Notification
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationResponse>
    {
        private readonly IMessageBus _messageBus;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CreateNotificationCommandHandler(IMessageBus messageBus, IUserRepository userRepository, 
            ICompanyRepository companyRepository, INotificationRepository notificationRepository, IHttpContextAccessor httpContextAccessor)
        {
            _messageBus = messageBus;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _notificationRepository = notificationRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<CreateNotificationResponse> Handle(CreateNotificationCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.Get(command.UserSender, "Users", cancellationToken).Result;
            var company = _companyRepository.Get(user.CompanyId, "Companies", cancellationToken).Result;

            if (!NaoChegouNoLimiteDoContrato(company, cancellationToken))
                throw new LimiteDeNotificacoesAtingidaException();

            if (command.Type == NotificationTypeEnum.Email)
                if (!IsValidEmailUtil.Check(command.Receiver)) throw new Exception("Email de destino inválido.");

            if (command.Type == NotificationTypeEnum.SMS)
                if (!IsPhoneNumberValid(command.Receiver)) throw new Exception("Telefone de destino inválido.");

            command.Id = Guid.NewGuid();
            command.Ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            _messageBus.Publish("notifications", "send-notification", command);
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

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", "");
            string pattern = @"^\+?\d{0,2}\s?\(?\d{2,}\)?[-\s]?\d{5,}[-\s]?\d{4}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }

    }
}
