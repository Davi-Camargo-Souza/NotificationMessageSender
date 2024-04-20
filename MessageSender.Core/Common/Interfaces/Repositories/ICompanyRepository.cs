using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.Core.Common.Interfaces.Repositories
{
    public interface ICompanyRepository : IBaseRepository<CompanyEntity>
    {
        Task<List<CompanyEntity>> GetAllCompanyByContract(ContractEnum contract, CancellationToken cancellationToken);
        Task<CompanyEntity> GetCompanyByEmail(string email, CancellationToken cancellationToken);
        Task<CompanyEntity> GetCompanyByCnpj(string cnpj, CancellationToken cancellationToken);
    }
}
