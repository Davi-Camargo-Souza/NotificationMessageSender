﻿using NotificationMessageSender.Core.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMessageSender.Core.Common.Interfaces.Repositories
{
    public interface INotificationRepository : IBaseRepository<NotificationEntity>
    {
        public Task<List<NotificationEntity>> GetAllRequestsOfDayByCompany(DateOnly date, Guid companyId, CancellationToken cancellationToken);
        public Task<List<NotificationEntity>> GetAllSentNotificationsByUser(Guid userId, CancellationToken cancellationToken);

    }
}
