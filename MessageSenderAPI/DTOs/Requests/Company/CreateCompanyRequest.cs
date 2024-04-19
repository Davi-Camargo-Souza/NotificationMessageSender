using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.DTOs.Requests.Company
{
    public class CreateCompanyRequest
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ContractEnum Contract { get; set; }
    }
}
