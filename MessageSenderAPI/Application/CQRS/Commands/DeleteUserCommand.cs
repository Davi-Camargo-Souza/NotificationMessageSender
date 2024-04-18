using MediatR;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Commands
{
    public class DeleteUserCommand : IRequest<List<GetUserResponse>>
    {
        public DeleteUserCommand(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
    }
}
