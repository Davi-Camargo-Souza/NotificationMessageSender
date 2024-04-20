using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.Core.Common.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> GetUserByCpf(string cpf, CancellationToken cancellationToken);
        public void Update(UserEntity entity);
        public Task<List<UserEntity>> GetAllUsersByCompany(Guid companyId, CancellationToken cancellationToken);


    }
}
