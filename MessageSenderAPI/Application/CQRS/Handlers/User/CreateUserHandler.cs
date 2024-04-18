using AutoMapper;
using MediatR;
using MessageSender.Core.Common.Domain.Entities;
using MessageSender.Core.Common.Interfaces;
using MessageSender.Core.Common.Uteis;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;

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
