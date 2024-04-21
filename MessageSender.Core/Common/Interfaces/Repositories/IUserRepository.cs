using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.Core.Common.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        public Task<UserEntity> GetUserByCpf(string cpf, CancellationToken cancellationToken);
        public Task<List<UserEntity>> GetAllUsersByCompany(Guid companyId, CancellationToken cancellationToken);
        public void Update(UserEntity entity);
    }
}
