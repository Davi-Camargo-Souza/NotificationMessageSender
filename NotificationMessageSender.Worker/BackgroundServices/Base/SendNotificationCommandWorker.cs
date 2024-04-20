using MediatR;
using Microsoft.Extensions.Hosting;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;

namespace NotificationMessageSender.Worker.BackgroundServices.Base
{
    public abstract class SendNotificationCommandWorker : BackgroundService
    {
        protected readonly IMediator _mediator;

        public SendNotificationCommandWorker(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task Process (SendNotificationCommand command)
        {
            await _mediator.Send(command);
        } 
    }
}
