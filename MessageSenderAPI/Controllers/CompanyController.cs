using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationMessageSender.API.Application.CQRS.Commands.Company;
using NotificationMessageSender.API.DTOs.Requests.Company;
using NotificationMessageSender.API.DTOs.Responses.Company;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;
using NotificationMessageSender.Core.Common.Enums;
using System.Reflection;

namespace NotificationMessageSender.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
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

                return Created($"api/Company/{serviceResponse.Dados.Id}", serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return BadRequest(serviceResponse);
        }


    }
}
