using MediatR;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.Core.Common.Enums;
using System.Security.Claims;

namespace NotificationMessageSender.API.Application.CQRS.Commands.Notification
{
    public class CreateNotificationCommand : IRequest<CreateNotificationResponse>
    {
        protected CreateNotificationCommand() { }
        public CreateNotificationCommand(NotificationRequest request)
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
        public Guid Id { get; set; }

    }
}
