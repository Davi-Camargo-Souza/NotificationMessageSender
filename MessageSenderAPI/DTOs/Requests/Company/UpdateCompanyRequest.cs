using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.DTOs.Requests.Company
{
    public class UpdateCompanyRequest
    {
        public string Cnpj { get; set; }
        public ContractEnum Contract { get; set; }
    }
}
