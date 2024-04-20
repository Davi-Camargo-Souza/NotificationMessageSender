using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces.Data;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Uteis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Company
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, DeleteCompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCompanyHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;

        }

        public async Task<DeleteCompanyResponse> Handle(DeleteCompanyCommand command, CancellationToken cancellationToken)
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
            if (queryResult.Name == "Empresa Teste") throw new Exception("Não é permitido apagar a empresa teste apartir do endpoint.");

            var users = await _userRepository.GetAllUsersByCompany(queryResult.Id, cancellationToken);
            if (users != null)
            {
                foreach (UserEntity user in users)
                {
                    await _userRepository.Delete(user);
                }
            }

            await _companyRepository.Delete(queryResult);
            await _unitOfWork.Commit(cancellationToken);

            return new DeleteCompanyResponse { Companies = await _companyRepository.GetAll("Companies", cancellationToken) };
        }
    }
}
