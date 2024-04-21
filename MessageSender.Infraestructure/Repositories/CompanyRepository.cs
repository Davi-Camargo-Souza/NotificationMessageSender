using Dapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Infraestructure.Context;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;

namespace NotificationMessageSender.Infraestructure.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context, DapperContext dapper) : base(context, dapper)
        {
        }

        public async Task<CompanyEntity> GetCompanyByCnpj(string cnpj, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Companies\"" +
                          $"WHERE \"Cnpj\" = '{cnpj}'";
                var queryResult = await connection.QueryFirstOrDefaultAsync<CompanyEntity>(sql, cancellationToken);
                connection.Close();
                return queryResult;
            }
        }

        public async Task<CompanyEntity> GetCompanyByEmail(string email, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Companies\"" +
                          $"WHERE \"Email\" = '{email}'";
                var queryResult = await connection.QueryFirstOrDefaultAsync<CompanyEntity>(sql, cancellationToken);
                connection.Close();
                return queryResult;
            }
        }

        public void Update(CompanyEntity entity)
        {
            entity.UpdatedAt = DateTime.Now.ToUniversalTime();
            _context.Update(entity);
        }
    }
}
