using MediatR;
using NotificationMessageSender.API.DTOs.Responses.User;
using AutoMapper;
using NotificationMessageSender.Core.Common.Uteis;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.API.Application.CQRS.Commands.User;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Interfaces.Data;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.User
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<GetUserResponse> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            UserEntity entity;
            Guid result;
            if (Guid.TryParse(command.Id, out result))
            {
                entity = _userRepository.Get(result, "Users", cancellationToken).Result;
            } else
            {
                entity = await _userRepository.GetUserByCpf(command.Id, cancellationToken);
            }

            if (entity == null) throw new Exception("Usuário não encontrado.");

            entity.Name = command.Name;
            entity.Password = PasswordUtil.HashPassword(command.Password);
            _userRepository.Update(entity);
            await _unitOfWork.Commit(cancellationToken);

            entity = _userRepository.Get(entity.Id, "Users", cancellationToken).Result;

            return _mapper.Map<GetUserResponse>(entity);
        }
    }
}
