using MediatR;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Core.Common.Exceptions;
using NotificationMessageSender.Core.Common.Interfaces;
using NotificationMessageSender.Core.Common.Interfaces.Data;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Interfaces.Services;
using System.Text.RegularExpressions;
using Vonage;
using Vonage.Messages.WhatsApp;
using Vonage.Messaging;
using Vonage.Request;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;
using Microsoft.AspNetCore.Http;
using NotificationMessageSender.Worker.Application.Commands.Send.Base;

namespace NotificationMessageSender.Worker.Application.Commands.Send.Handlers
{
    public class SendNotificationHandler : IRequestHandler<SendNotificationCommand, SendNotificationResponse>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSenderService _emailSenderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public SendNotificationHandler(INotificationRepository notificationRepository, ICompanyRepository companyRepository, 
            IUserRepository userRepository, IEmailSenderService emailSenderService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _emailSenderService = emailSenderService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }

        public async Task<SendNotificationResponse> Handle(SendNotificationCommand command, CancellationToken cancellationToken)
        {
            var user = _userRepository.Get(command.UserSender, "Users", cancellationToken).Result;
            var company = _companyRepository.Get(user.CompanyId, "Companies", cancellationToken).Result;

            if (command.Type == NotificationTypeEnum.Email)
            {
                await _emailSenderService.SendEmailAsync(new EmailRequest(
                    company.Email,
                        command.Receiver,
                        command.Subject,
                        command.Message,
                        "t#st3S3nFF"));
            }

            if(command.Type == NotificationTypeEnum.SMS)
            {
                command.Message = command.Subject + "\r\n" + command.Message;
                var telephone = LimparFormatacaoTelefone(command.Receiver);
                if (!telephone.StartsWith("55")) telephone = "55" + telephone;
                command.Receiver = telephone;

                await SendSMS(command);
            }

            var notification = new NotificationsRequestEntity()
            {
                UserId = user.Id,
                Id = command.Id,
                Message = command.Subject + " " + command.Message,
                Receiver = command.Receiver,
                Ip = command.Ip,
                CompanyId = company.Id
            };

            _notificationRepository.Create(notification, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return new SendNotificationResponse() { Requests = _notificationRepository.GetAllSentNotificationsByUser(user.Id, cancellationToken).Result };
        }

        private async Task<SendSmsResponse> SendSMS(SendNotificationCommand command)
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

        public static string LimparFormatacaoTelefone(string numeroTelefone)
        {
            return Regex.Replace(numeroTelefone, @"[^\d]", "");
        }
    }
}
