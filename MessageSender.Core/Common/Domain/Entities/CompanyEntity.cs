using MessageSender.Core.Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageSender.Core.Common.Domain.Entities
{
    public class CompanyEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ContractEnum Contract {  get; set; }

    }
}
