using MediatR;
using NotificationMessageSender.API.DTOs.Requests.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.API.Application.CQRS.Commands.Company
{
    public class UpdateCompanyCommand : IRequest<UpdateCompanyResponse>
    {
        public UpdateCompanyCommand(UpdateCompanyRequest request, string id)
        {
            Cnpj = request.Cnpj;
            Id = id;
            Contract = request.Contract;
        }

        public string Id { get; set; }
        public string Cnpj { get; set; }
        public ContractEnum Contract { get; set; }

    }
}
