using MediatR;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Responses.Notification;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Notification
{
    public class GetNotificationHandler : IRequestHandler<GetNotificationQuery, GetNotificationResponse>
    {
        public Task<GetNotificationResponse> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
