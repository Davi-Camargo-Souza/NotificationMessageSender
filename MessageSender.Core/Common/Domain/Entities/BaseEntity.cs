using System.ComponentModel.DataAnnotations;

namespace NotificationMessageSender.Core.Common.Domain.Entities
{
    public class BaseEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
