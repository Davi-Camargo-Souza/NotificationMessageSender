using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.DTOs.Responses.Company
{
    public class CreateCompanyResponse : BaseResponse
    {
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ContractEnum Contract { get; set; }
    }
}
