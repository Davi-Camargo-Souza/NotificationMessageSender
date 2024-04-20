using MediatR;
using NotificationMessageSender.API.DTOs.Responses.Company;

namespace NotificationMessageSender.API.Application.CQRS.Commands.Company
{
    public class DeleteCompanyCommand : IRequest<DeleteCompanyResponse>
    {
        public DeleteCompanyCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
