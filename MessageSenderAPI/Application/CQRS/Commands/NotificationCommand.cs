using MediatR;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.Core.Common.Enums;
using System.Security.Claims;

namespace NotificationMessageSender.API.Application.CQRS.Commands
{
    public class NotificationCommand : IRequest<SendNotificationResponse>
    {
        protected NotificationCommand() { }
        public NotificationCommand(NotificationRequest request)
        {
            Type = request.Type;
            Message = request.Message;
            Receiver = request.Receiver;
            Subject = request.Subject;
        }

        public NotificationTypeEnum Type { get; set; }
        public string Message { get; set; }
        public string Receiver { get; set; }
        public Guid UserSender { get; set; }
        public string Subject { get; set; }

    }
}
