namespace NotificationMessageSender.API.DTOs.Responses.Notification
{
    public class CreateNotificationResponse
    {
        public CreateNotificationResponse(Guid id, DateTime createdAt)
        {
            Id = id;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
