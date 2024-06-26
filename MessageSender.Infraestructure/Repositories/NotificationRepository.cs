﻿using Dapper;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
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
    public class NotificationRepository : BaseRepository<NotificationEntity>, INotificationRepository
    {
        public NotificationRepository(AppDbContext context, DapperContext dapper) : base(context, dapper)
        {
        }

        public async Task<List<NotificationEntity>> GetAllRequestsOfDayByCompany(DateOnly date, Guid companyId, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Notifications\"" +
                          $"WHERE DATE(\"SentAt\") = '{date}'" +
                          $"AND \"CompanyId\" = '{companyId}'";
                var queryResult = await connection.QueryAsync<NotificationEntity>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }

        public async Task<List<NotificationEntity>> GetAllSentNotificationsByUser (Guid userId, CancellationToken cancellationToken)
        {
            using (var connection = _dapper.CreateConnection())
            {
                connection.Open();
                var sql = $"SELECT *" +
                          $"FROM \"Notifications\"" +
                          $"WHERE \"UserId\" = '{userId}'";
                var queryResult = await connection.QueryAsync<NotificationEntity>(sql, cancellationToken);
                connection.Close();
                if (queryResult == null) return null;
                return queryResult.ToList();
            }
        }
    }
}
