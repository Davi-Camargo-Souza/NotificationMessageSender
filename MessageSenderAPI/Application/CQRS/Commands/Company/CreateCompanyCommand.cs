using MediatR;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.API.DTOs.Requests.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;

namespace NotificationMessageSender.API.Application.CQRS.Commands.Company
{
    public class CreateCompanyCommand : IRequest<CreateCompanyResponse>
    {
        protected CreateCompanyCommand() { }
        public CreateCompanyCommand(CreateCompanyRequest request)
        {
            Name = request.Name;
            Cnpj = request.Cnpj;
            Email = request.Email;
            Contract = request.Contract;
        }

        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public ContractEnum Contract { get; set; }
    }
}
