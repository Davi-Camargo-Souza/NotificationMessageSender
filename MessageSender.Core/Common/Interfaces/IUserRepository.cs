﻿using NotificationMessageSender.Core.Common.Domain.Entities;

namespace NotificationMessageSender.Core.Common.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity> GetUserByCpf(string cpf, CancellationToken cancellationToken);
        public void Update(UserEntity entity);

    }
}
