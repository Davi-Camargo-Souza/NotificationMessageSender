using MediatR;
using NotificationMessageSender.API.DTOs.Requests.User;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Commands.User
{
    public class CreateUserCommand : IRequest<CreateUserResponse>
    {
        protected CreateUserCommand() { }

        public CreateUserCommand(CreateUserRequest request)
        {
            Name = request.Name;
            Cpf = request.Cpf;
            Password = request.Password;
            CompanyId = request.CompanyId;
        }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public Guid CompanyId { get; set; }
    }
}
