using MediatR;
using NotificationMessageSender.API.DTOs.Responses.Company;

namespace NotificationMessageSender.API.Application.CQRS.Queries.Company
{
    public class GetAllCompaniesQuery : IRequest<GetAllCompaniesResponse>
    {
    }
}
