using MessageSender.Core.Common.Domain.Entities;
using MessageSender.Core.Common.Enums;

namespace MessageSender.Core.Common.Interfaces
{
    public interface ICompanyRepository : IBaseRepository<CompanyEntity>
    {
        Task<List<CompanyEntity>> GetAllCompanyByContract(ContractEnum contract, CancellationToken cancellationToken);
        Task<CompanyEntity> GetCompanyByEmail(string email, CancellationToken cancellationToken);
        Task<CompanyEntity> GetCompanyByCnpj(string cnpj, CancellationToken cancellationToken);
    }
}
