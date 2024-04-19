using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.DTOs;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Core.Common.Exceptions;
using NotificationMessageSender.Core.Common.Interfaces;
using System.Text.RegularExpressions;
using Vonage;
using Vonage.Messages.WhatsApp;
using Vonage.Messaging;
using Vonage.Request;

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

            if (command.Type == NotificationTypeEnum.Email)
            {
                if (!IsValidEmail(command.Receiver)) throw new Exception("Email de destino inválido.");

                await _emailSender.SendEmailAsync(new EmailRequest(
                    company.Email,
                        command.Receiver,
                        command.Subject,
                        command.Message,
                        "t#st3S3nFF"));
            }

            if(command.Type == NotificationTypeEnum.SMS)
            {
                if (!IsPhoneNumberValid(command.Receiver)) throw new Exception("Telefone de destino inválido.");

                command.Message = command.Subject + "\r\n" + command.Message;

                var telephone = LimparFormatacaoTelefone(command.Receiver);

                if (!telephone.StartsWith("55")) telephone = "55" + telephone;

                command.Receiver = telephone;

                await SendSMS(command);
            }

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

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@([a-z\d]+(-[a-z\d]+)*\.)+[a-z]{2,}$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private async Task<Vonage.Messaging.SendSmsResponse> SendSMS(NotificationCommand command)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
                "e83c42fa",
                "014XT3EN7XzDC3Iq"
                );

            var vonageClient = new VonageClient(credentials);

            var response = await vonageClient.SmsClient.SendAnSmsAsync(new SendSmsRequest()
            {
                To = command.Receiver,
                From = "Vonage APIs",
                Text = command.Message
            });

            return response;
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(" ", "");
            string pattern = @"^\+?\d{0,2}\s?\(?\d{2,}\)?[-\s]?\d{5,}[-\s]?\d{4}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }

        public static string LimparFormatacaoTelefone(string numeroTelefone)
        {
            string numeroLimpo = Regex.Replace(numeroTelefone, @"[^\d]", "");

            return numeroLimpo;
        }
    }
}
