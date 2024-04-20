using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationMessageSender.Core.MessageBus.Services.Interfaces
{
    public interface IMessageBus : IDisposable
    {
        void Publish(string exchange, string routingKey, dynamic command);
        void Subscribe<TMessage>(string exchange, string routingKey, Func<TMessage, Task> function, CancellationToken stoppingToken);
    }
}
