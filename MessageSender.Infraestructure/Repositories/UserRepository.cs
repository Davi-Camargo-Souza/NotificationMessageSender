using Dapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Infraestructure.Context;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;

namespace NotificationMessageSender.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {

        public UserRepository(AppDbContext context, DapperContext dapper) : base(context, dapper)
        {
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

        public async Task<List<UserEntity>> GetAllUsersByCompany (Guid companyId, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Users\"" +
                          $"WHERE \"CompanyId\" = '{companyId}'";
                var queryResult = await connection.QueryAsync<UserEntity>(sql, cancellationToken);
                connection.Close();
                return queryResult.ToList();
            }
        }

        public void Update(UserEntity entity)
        {
            entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            _context.Update(entity);
        }


    }
}
