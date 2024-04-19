using Dapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces;
using NotificationMessageSender.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationMessageSender.Infraestructure.Repositories
{
    public class NotificationRepository : BaseRepository<NotificationsRequestEntity>, INotificationRepository
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapper;
        public NotificationRepository(AppDbContext context, DapperContext dapper) : base(context, dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<List<NotificationsRequestEntity>> GetAllRequestsOfDayByCompany(DateOnly date, Guid companyId, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"NotificationsRequests\"" +
                          $"WHERE DATE(\"CreatedAt\") = '{date}'" +
                          $"AND \"CompanyId\" = '{companyId}'";
                var queryResult = await connection.QueryAsync<NotificationsRequestEntity>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }

        public async Task<List<NotificationsRequestEntity>> GetAllSentNotificationsByUser (Guid userId, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"NotificationsRequests\"" +
                          $"WHERE \"UserId\" = '{userId}'";
                var queryResult = await connection.QueryAsync<NotificationsRequestEntity>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }
    }
}
