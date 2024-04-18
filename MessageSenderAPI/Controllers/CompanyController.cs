using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Requests.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;
using System.Reflection;

namespace NotificationMessageSender.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CompanyController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
           _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CreateCompanyResponse>>> CreateCompany (CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            ServiceResponse<CreateCompanyResponse> serviceResponse = new ServiceResponse<CreateCompanyResponse>();
            try
            {
                var command = _mapper.Map<CreateCompanyCommand>(request);
                serviceResponse.Dados = await _mediator.Send(command);
            } catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }


    }
}
