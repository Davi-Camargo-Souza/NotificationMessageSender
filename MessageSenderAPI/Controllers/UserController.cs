using AutoMapper;
using MediatR;
using MessageSender.Core.Common.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Requests.User;
using NotificationMessageSender.API.DTOs.Responses.User;
using NotificationMessageSender.Core.Common;
using System.Collections.Generic;

namespace MessageSender.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<ServiceResponse<CreateUserResponse>>> CreateUser (CreateUserRequest request)
        {
            ServiceResponse<CreateUserResponse> serviceResponse = new ServiceResponse<CreateUserResponse> ();
            try
            {
                var command = new CreateUserCommand(request);
                serviceResponse.Dados = await _mediator.Send(command);
            } catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return Created($"api/User/{serviceResponse.Dados.Id}",serviceResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserResponse>>> GetUser(string id)
        {
            ServiceResponse<GetUserResponse> serviceResponse = new ServiceResponse<GetUserResponse>();
            try
            {
                var query = new GetUserQuery(id);
                serviceResponse.Dados = await _mediator.Send(query);
            } catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return Ok(serviceResponse);
        }

        [HttpGet("AllUsers")]
        public async Task<ActionResult<ServiceResponse<List<GetUserResponse>>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserResponse>> serviceResponse = new ServiceResponse<List<GetUserResponse>>();
            try
            {
                GetAllUsersQuery query = new GetAllUsersQuery();
                serviceResponse.Dados = await _mediator.Send(query);
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return Ok(serviceResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserResponse>>> UpdateUser(string id, [FromBody] UpdateUserRequest request)
        {
            ServiceResponse<GetUserResponse> serviceResponse = new ServiceResponse<GetUserResponse>();
            try
            {
                var query = new GetUserQuery(id);
                
                await _mediator.Send(query);

                UpdateUserCommand command = new UpdateUserCommand(id, request);

                serviceResponse.Dados = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return Ok(serviceResponse);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetUserResponse>>>> DeleteUser (string id)
        {
            ServiceResponse<List<GetUserResponse>> serviceResponse = new ServiceResponse<List<GetUserResponse>>();
            try
            {
                var command = new DeleteUserCommand(id);
                serviceResponse.Dados = await _mediator.Send(command);
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return Ok(serviceResponse);
        }

    }
}
