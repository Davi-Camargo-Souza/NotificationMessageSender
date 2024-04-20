using MediatR;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.Worker.Application.Commands.Send.Base
{
    public class SendNotificationCommand : IRequest<SendNotificationResponse>
    {
        protected SendNotificationCommand() { }

        public SendNotificationCommand(NotificationTypeEnum type, string message, string receiver, Guid userSender, string subject, Guid id)
        {
            Type = type;
            Message = message;
            Receiver = receiver;
            UserSender = userSender;
            Subject = subject;
            Id = id;
        }

        public NotificationTypeEnum Type { get; set; }
        public string Message { get; set; }
        public string Receiver { get; set; }
        public Guid UserSender { get; set; }
        public string Subject { get; set; }
        public Guid Id { get; set; }
    }
}
