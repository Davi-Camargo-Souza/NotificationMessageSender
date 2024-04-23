using MediatR;
using NotificationMessageSender.API.Application.CQRS.Queries.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.API.Application.Uteis;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Company
{
    public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, GetCompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<GetCompanyResponse> Handle(GetCompanyQuery query, CancellationToken cancellationToken)
        {
            CompanyEntity queryResult;
            Guid result;
            if (Guid.TryParse(query.Id, out result))
            {
                queryResult = await _companyRepository.Get(result, "Companies", cancellationToken);
            }
            else if (IsValidEmailUtil.Check(query.Id))
            {
                queryResult = await _companyRepository.GetCompanyByEmail(query.Id, cancellationToken);
            }
            else
            {
                 queryResult = await _companyRepository.GetCompanyByCnpj(query.Id, cancellationToken);
            }

            if (queryResult == null) throw new Exception("Empresa não encontrada.");
            return new GetCompanyResponse(queryResult);
        }
    }
}
