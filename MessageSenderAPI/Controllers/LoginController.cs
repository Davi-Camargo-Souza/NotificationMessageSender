using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.Core.Common;

namespace NotificationMessageSender.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<LoginResponse>>> Login (LoginRequest request)
        {
            ServiceResponse<LoginResponse> serviceResponse = new ServiceResponse<LoginResponse>();
            try
            {
                var command = new LoginCommand(request);
                serviceResponse.Dados = await _mediator.Send(command);

                return Ok(serviceResponse);
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
