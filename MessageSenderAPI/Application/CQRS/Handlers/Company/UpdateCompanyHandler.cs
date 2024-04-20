using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces.Data;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Uteis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Company
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, UpdateCompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateCompanyResponse> Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
        {
            CompanyEntity queryResult;
            Guid result;
            if (Guid.TryParse(command.Id, out result))
            {
                queryResult = await _companyRepository.Get(result, "Companies", cancellationToken);
            }
            else if (IsValidEmailUtil.Check(command.Id))
            {
                queryResult = await _companyRepository.GetCompanyByEmail(command.Id, cancellationToken);
            }
            else
            {
                queryResult = await _companyRepository.GetCompanyByCnpj(command.Id, cancellationToken);
            }

            if (queryResult == null) throw new Exception("Empresa não encontrada.");

            if (command.Cnpj != null )
            {
                if ( _companyRepository.GetCompanyByCnpj(command.Cnpj, cancellationToken).Result.Id != queryResult.Id)
                {
                    throw new Exception("Cnpj passado já pertence a outra empresa cadastrada.");
                }
                queryResult.Cnpj = command.Cnpj;
            }

            queryResult.Contract = command.Contract;

            _companyRepository.Update(queryResult);
            await _unitOfWork.Commit(cancellationToken);

            return new UpdateCompanyResponse { Companies = _companyRepository.GetAll("Companies", cancellationToken).Result.ToList() };
        }
    }
}
