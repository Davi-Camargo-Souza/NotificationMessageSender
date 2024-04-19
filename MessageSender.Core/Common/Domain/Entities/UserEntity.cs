using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.Core.Common.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
        public string[] Roles { get; set; } = new string[] {RolesEnum.user.ToString()};
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        //public bool Ativo { get; set; } = true;
    }
}
