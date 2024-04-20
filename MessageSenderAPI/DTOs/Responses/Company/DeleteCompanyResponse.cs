using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.API.DTOs.Responses.Company
{
    public class DeleteCompanyResponse
    {
        public List<CompanyEntity> Companies { get; set; }
    }
}
