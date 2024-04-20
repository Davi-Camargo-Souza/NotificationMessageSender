using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.API.DTOs.Responses.Notification
{
    public class GetNotificationResponse
    {
        public NotificationsRequestEntity Notification { get; set; }
    }
}
