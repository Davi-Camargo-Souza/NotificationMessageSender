using MediatR;
using Microsoft.Extensions.Hosting;
using NotificationMessageSender.API.Application.CQRS.Commands.Notification;
using NotificationMessageSender.Core.MessageBus.Services.Interfaces;
using NotificationMessageSender.Worker.BackgroundServices.Base;


namespace NotificationMessageSender.Worker.BackgroundServices
{
    public class SendNotificationWorker : SendNotificationCommandWorker
    {
        private readonly IMessageBus _messageBus;

        public SendNotificationWorker(IMessageBus messageBus, IServiceProvider serviceProvider) : base (serviceProvider)
        {
            _messageBus = messageBus;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _messageBus.Subscribe<SendNotificationCommand>("notifications","send-notification", Process, stoppingToken);
            await Task.Delay(-1, stoppingToken);

        }
    }
}
