using MediatR;
using NotificationMessageSender.API.DTOs.Requests.User;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Commands
{
    public class UpdateUserCommand : IRequest<GetUserResponse>
    {
        protected UpdateUserCommand() { }

        public UpdateUserCommand(string id, UpdateUserRequest request)
        {
            Id = id;
            Name = request.Name;
            Password = request.Password;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
