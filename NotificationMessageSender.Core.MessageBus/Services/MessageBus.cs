using NotificationMessageSender.Core.MessageBus.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace NotificationMessageSender.Core.MessageBus.Services
{
    public class MessageBus : IMessageBus
    {
        private readonly ConnectionFactory _connectionFactory;
        private bool _isConnected = false;
        private IConnection _connection;
        private IModel _consumerChannel;

        private readonly IDictionary<string, string> _exchange = new Dictionary<string, string>();
        private readonly IDictionary<string, string> _routingKeys = new Dictionary<string, string>();


        public MessageBus()
        {
            _connectionFactory = new ConnectionFactory { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        }

        private IConnection connection
        {
            get
            {
                if (!_isConnected)
                {
                    Connect();
                }

                return _connection;
            }
        }

        public void Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            _isConnected = true;
        }


        public void Dispose()
        {
            _connection.Dispose();
            
        }

        public void Publish(string exchange, string routingKey, dynamic command)
        {
            var channel = connection.CreateModel();

            DeclareExchangeAndQueue(exchange, routingKey, channel);

            string commandJson = JsonSerializer.Serialize(command);
            var messageBodyBytes = Encoding.UTF8.GetBytes(commandJson);

            channel.BasicPublish(exchange, routingKey, null, messageBodyBytes);
        }

        public void DeclareExchangeAndQueue (string exchange, string routingKey, IModel channel)
        {
            channel.ExchangeDeclare(exchange, ExchangeType.Direct, true);
            _exchange.Add(exchange, exchange);

            channel.QueueDeclare(routingKey, true, false, false, null);
            channel.QueueBind(routingKey, exchange, routingKey);

            _routingKeys.Add(routingKey, routingKey);
        }

        public void Subscribe<TMessage>(string exchange, string routingKey, Func<TMessage, Task> function, CancellationToken stoppingToken)
        {
            _consumerChannel = connection.CreateModel();

            DeclareExchangeAndQueue(exchange, routingKey, _consumerChannel);
            _consumerChannel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_consumerChannel);

            consumer.Received += async (sender, eventArgs) =>
            {
                try
                {
                    var messageBody = eventArgs.Body.ToArray();
                    string messageJson = Encoding.UTF8.GetString(messageBody);
                    var args = JsonSerializer.Deserialize<TMessage>(messageJson);

                    await 

                    _consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
                } catch (Exception ex)
                {
                    _consumerChannel.BasicNack(eventArgs.DeliveryTag, false, true);
                }
            };

            var consumerTag = _consumerChannel.BasicConsume(routingKey, false, consumer);
            _consumerChannel.BasicCancel(consumerTag);






        }
    }
}
