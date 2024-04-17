namespace MessageSender.Core.Common.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
    }
}
