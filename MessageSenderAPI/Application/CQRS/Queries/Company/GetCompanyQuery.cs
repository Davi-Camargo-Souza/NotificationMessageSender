using MediatR;
using NotificationMessageSender.API.DTOs.Responses.Company;

namespace NotificationMessageSender.API.Application.CQRS.Queries.Company
{
    public class GetCompanyQuery : IRequest<GetCompanyResponse>
    {
        public GetCompanyQuery(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
