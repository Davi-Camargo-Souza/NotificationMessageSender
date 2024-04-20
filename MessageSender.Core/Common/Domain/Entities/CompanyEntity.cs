using NotificationMessageSender.Core.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationMessageSender.Core.Common.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ContractEnum Contract {  get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        //public bool Ativo { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
