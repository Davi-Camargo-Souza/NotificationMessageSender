﻿using Dapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Infraestructure.Context;
using NotificationMessageSender.Core.Common.Interfaces;

namespace NotificationMessageSender.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapper;

        public UserRepository(AppDbContext context, DapperContext dapper) : base(context, dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<UserEntity> GetUserByCpf(string cpf, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Users\"" +
                          $"WHERE \"Cpf\" = '{cpf}'";
                var queryResult = await connection.QueryFirstOrDefaultAsync<UserEntity>(sql, cancellationToken);
                connection.Close();
                return queryResult;
            }
        }

        public void Update(UserEntity entity)
        {
            entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            _context.Update(entity);
        }


    }
}
