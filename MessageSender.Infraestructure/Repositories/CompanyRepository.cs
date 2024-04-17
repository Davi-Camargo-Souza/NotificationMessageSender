using Dapper;
using MessageSender.Core.Common.Domain.Entities;
using MessageSender.Core.Common.Enums;
using MessageSender.Infraestructure.Context;
using MessageSender.Core.Common.Interfaces;

namespace MessageSender.Infraestructure.Repositories
{
    public class CompanyRepository : BaseRepository<CompanyEntity>, ICompanyRepository
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapper;
        public CompanyRepository(AppDbContext context, DapperContext dapper) : base(context, dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<List<CompanyEntity>> GetAllCompanyByContract(ContractEnum contract, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Companies\"" +
                          $"WHERE \"Contract\" = '{contract}'";
                var queryResult = await connection.QueryAsync<CompanyEntity>(sql, cancellationToken); 
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }

        public async Task<CompanyEntity> GetCompanyByCnpj(string cnpj, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Companies\"" +
                          $"WHERE \"Cnpj\" = '{cnpj}'";
                var queryResult = await connection.QueryFirstAsync(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) throw new Exception("Registro não encontrado");
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
                var queryResult = await connection.QueryFirstAsync(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) throw new Exception("Registro não encontrado");
                return queryResult;
            }
        }
    }
}
