using AutoMapper;
using MediatR;
using MessageSender.Core.Common.Domain.Entities;
using MessageSender.Core.Common.Interfaces;
using MessageSender.Infraestructure.Repositories;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Responses.User;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.User
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<GetUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<GetUserResponse>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var list = _userRepository.GetAll("Users", cancellationToken).Result;

            return list.Select(userEntity => _mapper.Map<GetUserResponse>(userEntity)).ToList();
        }
    }
}
