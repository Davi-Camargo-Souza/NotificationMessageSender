using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.DTOs.Requests
{
    public class NotificationRequest
    {
        public NotificationTypeEnum Type { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Receiver { get; set; }
    }
}
