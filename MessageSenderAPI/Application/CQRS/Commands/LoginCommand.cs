using MediatR;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses;

namespace NotificationMessageSender.API.Application.CQRS.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginCommand(LoginRequest request)
        {
            Cpf = request.Cpf;
            Password = request.Password;
        }
        public string Cpf { get; set; }
        public string Password { get; set; }
    }
}
