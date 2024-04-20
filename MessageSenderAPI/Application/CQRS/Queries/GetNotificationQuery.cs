using MediatR;
using NotificationMessageSender.API.DTOs.Responses.Notification;

namespace NotificationMessageSender.API.Application.CQRS.Queries
{
    public class GetNotificationQuery : IRequest<GetNotificationResponse>
    {
        public GetNotificationQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
