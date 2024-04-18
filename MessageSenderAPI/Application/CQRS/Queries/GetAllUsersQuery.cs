using MediatR;
using MessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Queries
{
    public class GetAllUsersQuery : IRequest<List<GetUserResponse>>
    {
    }
}
