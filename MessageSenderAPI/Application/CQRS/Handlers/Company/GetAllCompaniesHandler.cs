using MediatR;
using NotificationMessageSender.API.Application.CQRS.Queries.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Company
{
    public class GetAllCompaniesHandler : IRequestHandler<GetAllCompaniesQuery, GetAllCompaniesResponse>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetAllCompaniesHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetAllCompaniesResponse> Handle(GetAllCompaniesQuery query, CancellationToken cancellationToken)
        {
            return new GetAllCompaniesResponse { Companies = _companyRepository.GetAll("Companies", cancellationToken).Result.ToList() };
        }
    }
}
