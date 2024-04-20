using MediatR;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.Application.CQRS.Commands.Notification
{
    public class SendNotificationCommand : IRequest<SendNotificationResponse>
    {
        protected SendNotificationCommand() { }
        public SendNotificationCommand(CreateNotificationCommand request)
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
