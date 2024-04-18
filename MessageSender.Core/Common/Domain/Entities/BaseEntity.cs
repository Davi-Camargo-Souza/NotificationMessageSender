using System.ComponentModel.DataAnnotations;

namespace MessageSender.Core.Common.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public bool Ativo {  get; set; } = true;

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
