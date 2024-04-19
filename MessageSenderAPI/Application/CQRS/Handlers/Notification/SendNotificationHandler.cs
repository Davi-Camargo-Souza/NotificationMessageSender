using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.DTOs;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Core.Common.Exceptions;
using NotificationMessageSender.Core.Common.Interfaces;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Notification
{
    public class SendNotificationHandler : IRequestHandler<NotificationCommand, SendNotificationResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public SendNotificationHandler(INotificationRepository notificationRepository, ICompanyRepository companyRepository, 
            IUserRepository userRepository, IEmailSender emailsender, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _emailSender = emailsender;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<SendNotificationResponse> Handle(NotificationCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.Get(command.UserSender, "Users", cancellationToken).Result;
            var company = _companyRepository.Get(user.CompanyId, "Companies", cancellationToken).Result;

            if (!NaoChegouNoLimiteDoContrato(company, cancellationToken)) 
                throw new LimiteDeNotificacoesAtingidaException();
           
            await _emailSender.SendEmailAsync(new EmailRequest(
                    company.Email,
                        command.Receiver,
                        command.Subject,
                        command.Message,
                        "t#st3S3nFF"));

            var notification = new NotificationsRequestEntity()
            {
                UserId = user.Id,
                Message = command.Message,
                Receiver = command.Receiver,
                Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                CompanyId = company.Id
            };

            _notificationRepository.Create(notification, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            var response = new SendNotificationResponse()
            {
                Requests = _notificationRepository.GetAllSentNotificationsByUser(user.Id, cancellationToken).Result
            };

            return response;
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

            if (limitesPorContrato.TryGetValue(company.Contract, out int limite))
            {
                if (notificationsRequests.Count < limite)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
