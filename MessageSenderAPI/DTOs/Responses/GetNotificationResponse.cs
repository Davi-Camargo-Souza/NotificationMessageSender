using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.API.DTOs.Responses
{
    public class GetNotificationResponse
    {
        public NotificationsRequestEntity Notification { get; set; }
    }
}
