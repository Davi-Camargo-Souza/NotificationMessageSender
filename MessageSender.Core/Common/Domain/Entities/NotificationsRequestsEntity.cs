using System.ComponentModel.DataAnnotations;

namespace MessageSender.Core.Common.Domain.Entities
{
    public class NotificationsRequestsEntity 
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Ip {  get; set; }
        public DateTime RequestTime { get; set; } = DateTime.Now;
        public string Message { get; set; }

    }
}
