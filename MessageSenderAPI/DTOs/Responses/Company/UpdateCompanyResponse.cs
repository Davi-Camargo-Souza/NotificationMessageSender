using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.API.DTOs.Responses.Company
{
    public class UpdateCompanyResponse
    {
        public List<CompanyEntity> Companies { get; set; }
    }
}
