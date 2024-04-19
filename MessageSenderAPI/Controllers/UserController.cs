using AutoMapper;
using MediatR;
using NotificationMessageSender.Core.Common.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Requests.User;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;
using System.Collections.Generic;
using NotificationMessageSender.API.Application.CQRS.Commands.User;
using NotificationMessageSender.API.Application.CQRS.Queries.User;
using Microsoft.AspNetCore.Authorization;

namespace NotificationMessageSender.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ServiceResponse<CreateUserResponse>>> CreateUser (CreateUserRequest request)
        {
            ServiceResponse<CreateUserResponse> serviceResponse = new ServiceResponse<CreateUserResponse> ();
            try
            {
                var command = new CreateUserCommand(request);
                serviceResponse.Dados = await _mediator.Send(command);

                return Created($"api/User/{serviceResponse.Dados.Id}",serviceResponse);
            } catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return BadRequest(serviceResponse);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserResponse>>> GetUser(string id)
        {
            ServiceResponse<GetUserResponse> serviceResponse = new ServiceResponse<GetUserResponse>();
            try
            {
                var query = new GetUserQuery(id);
                serviceResponse.Dados = await _mediator.Send(query);

                return Ok(serviceResponse);
            } catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return BadRequest(serviceResponse);
        }

        [HttpGet("AllUsers")]
        public async Task<ActionResult<ServiceResponse<List<GetUserResponse>>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserResponse>> serviceResponse = new ServiceResponse<List<GetUserResponse>>();
            try
            {
                GetAllUsersQuery query = new GetAllUsersQuery();
                serviceResponse.Dados = await _mediator.Send(query);

                return Ok(serviceResponse);
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return BadRequest(serviceResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserResponse>>> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            ServiceResponse<GetUserResponse> serviceResponse = new ServiceResponse<GetUserResponse>();
            try
            {
                var query = new GetUserQuery(id);
                await _mediator.Send(query);

                var command = new UpdateUserCommand(id, request);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserResponse>>>> DeleteUser (string id)
        {
            ServiceResponse<List<GetUserResponse>> serviceResponse = new ServiceResponse<List<GetUserResponse>>();
            try
            {
                var command = new DeleteUserCommand(id);
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
