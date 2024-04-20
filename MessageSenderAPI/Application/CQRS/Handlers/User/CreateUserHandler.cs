using AutoMapper;
using MediatR;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Uteis;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;
using NotificationMessageSender.API.Application.CQRS.Commands.User;
using NotificationMessageSender.Core.Common.Enums;
using NotificationMessageSender.Core.Common.Interfaces.Repositories;
using NotificationMessageSender.Core.Common.Interfaces.Data;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserHandler(IUserRepository userRepository, ICompanyRepository companyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var queryResult = await _userRepository.GetUserByCpf(request.Cpf, cancellationToken);
            if (queryResult != null) throw new Exception("Usuário já cadastrado");

            var query = _companyRepository.Get(request.CompanyId, "Companies", cancellationToken).Result;
            if (query == null) throw new Exception("Empresa inválida ou não cadastrada.");


            var password = PasswordUtil.HashPassword(request.Password);
            request.Password = password;

            var entity = _mapper.Map<UserEntity>(request);

            _userRepository.Create(entity, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateUserResponse>(entity);
        }
    }
}
