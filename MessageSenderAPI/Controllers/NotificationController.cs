﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationMessageSender.API.Application.CQRS.Commands;
using NotificationMessageSender.API.Application.CQRS.Queries;
using NotificationMessageSender.API.DTOs.Requests;
using NotificationMessageSender.API.DTOs.Responses;
using NotificationMessageSender.Core.Common;
using NotificationMessageSender.Core.Common.Exceptions;
using System.Security.Claims;

namespace NotificationMessageSender.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<SendNotificationResponse>>> SendNotification(NotificationRequest request)
        {
            var serviceResponse = new ServiceResponse<SendNotificationResponse>();
            try {
                var command = new NotificationCommand(request);
                var userId = User.FindFirst(ClaimTypes.Name)?.Value;

                if (userId == null) throw new Exception("Usuário não autenticado.");

                command.UserSender = Guid.Parse(userId);
                serviceResponse.Dados = await _mediator.Send(command);

                return Ok(serviceResponse);
            }
            catch (LimiteDeNotificacoesAtingidaException ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;

                return UnprocessableEntity(serviceResponse);
            } catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return BadRequest(serviceResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetNotificationResponse>>> GetNotification(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetNotificationResponse>();
            try
            {
                var query = new GetNotificationQuery(id);
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

    }
}
