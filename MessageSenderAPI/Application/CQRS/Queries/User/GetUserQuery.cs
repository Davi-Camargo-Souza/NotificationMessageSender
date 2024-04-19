using MediatR;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Queries.User
{
    public class GetUserQuery : IRequest<GetUserResponse>
    {
        public GetUserQuery(string id)
        {
            this.id = id;
        }
        public string id { get; set; }
    }
}
