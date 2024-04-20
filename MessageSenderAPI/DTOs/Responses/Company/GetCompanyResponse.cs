using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.DTOs.Responses.Company
{
    public class GetCompanyResponse
    {
        public GetCompanyResponse(CompanyEntity company)
        {
            Id = company.Id;
            Name = company.Name;
            Cnpj = company.Cnpj;
            Email = company.Email;
            Contract = company.Contract;
            UpdatedAt = company.UpdatedAt;
            CreatedAt = company.CreatedAt;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ContractEnum Contract { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();
    }
}
