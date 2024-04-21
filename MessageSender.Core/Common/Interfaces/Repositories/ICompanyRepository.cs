using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;

namespace NotificationMessageSender.Core.Common.Interfaces.Repositories
{
    public interface ICompanyRepository : IBaseRepository<CompanyEntity>
    {
        public Task<CompanyEntity> GetCompanyByEmail(string email, CancellationToken cancellationToken);
        public Task<CompanyEntity> GetCompanyByCnpj(string cnpj, CancellationToken cancellationToken);
        public void Update(CompanyEntity entity);
    }
}
