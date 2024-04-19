using AutoMapper;
using MediatR;
using NotificationMessageSender.Core.Common.Domain.Entities;
using NotificationMessageSender.Core.Common.Interfaces;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.Core.Common;
using NotificationMessageSender.API.Application.CQRS.Commands.Company;

namespace NotificationMessageSender.API.Application.CQRS.Handlers.Company
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyHandler(ICompanyRepository companyRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCompanyResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {

            if (_companyRepository.GetCompanyByEmail(request.Email, cancellationToken).Result != null ||
            _companyRepository.GetCompanyByCnpj(request.Cnpj, cancellationToken).Result != null)
            {
                throw new Exception("Empresa já cadastrada.");
            }

            var entity = _mapper.Map<CompanyEntity>(request);
            _companyRepository.Create(entity, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateCompanyResponse>(entity);
        }
    }
}
