using MediatR;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Responses.Notification;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Notification
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationQuery, GetNotificationResponse>
    {
        private readonly INotificationRepository _notificationRepository;

        public GetNotificationHandler(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<GetNotificationResponse> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            var notification = _notificationRepository.Get(request.Id, "Notifications", cancellationToken).Result;

            if (notification == null) { throw new Exception("Notificação não encontrada."); }
            return new GetNotificationResponse { Notification = notification };
        }
    }
}
