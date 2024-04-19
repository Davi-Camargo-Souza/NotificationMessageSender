using NotificationMessageSender.Core.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMessageSender.Core.Common.Interfaces
{
    public interface INotificationRepository : IBaseRepository<NotificationsRequestEntity>
    {
        public Task<List<NotificationsRequestEntity>> GetAllRequestsOfDayByCompany(DateOnly date, Guid companyId, CancellationToken cancellationToken);
        public Task<List<NotificationsRequestEntity>> GetAllSentNotificationsByUser(Guid userId, CancellationToken cancellationToken);

    }
}
