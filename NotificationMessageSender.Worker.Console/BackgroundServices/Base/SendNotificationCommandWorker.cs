using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;
using NotificationMessageSender.Worker.Application.Commands.Send.Base;

namespace NotificationMessageSender.Worker.BackgroundServices.Base
{
    public abstract class SendNotificationCommandWorker : BackgroundService
    {
        protected readonly IServiceProvider _serviceProvider;

        public SendNotificationCommandWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected async Task Process (SendNotificationCommand command)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(command);
            }

        } 
    }
}
