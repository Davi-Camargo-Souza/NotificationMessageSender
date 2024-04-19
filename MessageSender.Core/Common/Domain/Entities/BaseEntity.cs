using System.ComponentModel.DataAnnotations;

namespace NotificationMessageSender.Core.Common.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
