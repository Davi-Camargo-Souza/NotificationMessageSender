namespace NotificationMessageSender.API.DTOs.Responses
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
