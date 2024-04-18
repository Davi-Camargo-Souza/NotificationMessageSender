using MessageSender.Core.Common.Domain.Entities;

namespace MessageSender.Core.Common.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> GetUserByCpf(string cpf, CancellationToken cancellationToken);
    }
}
