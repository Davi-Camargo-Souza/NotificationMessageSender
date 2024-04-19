using MediatR;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Queries.User
{
    public class GetAllUsersQuery : IRequest<List<GetUserResponse>>
    {
    }
}
