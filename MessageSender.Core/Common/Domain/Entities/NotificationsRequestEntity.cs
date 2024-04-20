using System.ComponentModel.DataAnnotations;

namespace NotificationMessageSender.Core.Common.Domain.Entities
{
    public class NotificationsRequestEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string Ip {  get; set; }
        public string Message { get; set; }
        public string Receiver { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now.ToUniversalTime();

    }
}
