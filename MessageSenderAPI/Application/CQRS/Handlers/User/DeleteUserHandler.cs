using AutoMapper;
using MediatR;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.API.Application.CQRS.Commands.User;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Interfaces.Data;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.User
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, List<GetUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<List<GetUserResponse>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            UserEntity entity;
            Guid result;
            if (Guid.TryParse(command.Id, out result))
            {
                entity = await _userRepository.Get(result, "Users", cancellationToken);
            }
            else
            {
                entity = await _userRepository.GetUserByCpf(command.Id, cancellationToken);
            }

            if (entity == null) throw new Exception("Usuário não encontrado.");
            if (entity.Name == "admin") throw new Exception("Não é permitido apagar o usuário admin pelo endpoint.");

            await _userRepository.Delete(entity);
            await _unitOfWork.Commit(cancellationToken);

            var list = _userRepository.GetAll("Users", cancellationToken).Result;

            return list.Select(userEntity => _mapper.Map<GetUserResponse>(userEntity)).ToList();

        }
    }
}
