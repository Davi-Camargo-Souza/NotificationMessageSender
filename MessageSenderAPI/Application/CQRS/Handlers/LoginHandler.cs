using AutoMapper;
using MediatR;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.API.Services;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Interfaces.Services;
using NotificationMessageSender.API.Application.Uteis;
using System.Security.Authentication;

namespace NotificationMessageSender.API.Application.CQRS.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var entity = _userRepository.GetUserByCpf(command.Cpf, cancellationToken).Result;
            if (entity == null) throw new Exception("Usuário não encontrado.");

            var hashedCommandPassword = PasswordUtil.HashPassword(command.Password);

            if (PasswordUtil.HashPassword(hashedCommandPassword + entity.CreatedAt) !=
                PasswordUtil.HashPassword(entity.Password + entity.CreatedAt)) throw new InvalidCredentialException("Senha incorreta.");

            var response = _mapper.Map<LoginResponse>(entity);
            response.BearerToken = _tokenService.Generate(entity);

            return Task.FromResult(response);
        }
    }
}
