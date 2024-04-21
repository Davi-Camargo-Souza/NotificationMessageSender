using System.ComponentModel.DataAnnotations;

namespace NotificationMessageSender.Core.Common.Domain.Entities
{
    public abstract class BaseEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
