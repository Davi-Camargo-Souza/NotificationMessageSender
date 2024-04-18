using AutoMapper;
using MediatR;
using MessageSender.Core.Common.Domain.Entities;
using MessageSender.Core.Common.Interfaces;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.User
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetUserResponse> Handle(GetUserQuery command, CancellationToken cancellationToken)
        {
            UserEntity queryResult;
            Guid result;
            if (Guid.TryParse(command.id, out result))
            {
                queryResult = await _userRepository.Get(result, "Users", cancellationToken);
            }
            else
            {
                queryResult = await _userRepository.GetUserByCpf(command.id, cancellationToken);
            }

            if (queryResult == null) throw new Exception("Usuário não encontrado.");

            return _mapper.Map<GetUserResponse>(queryResult);
        }
    }
}
