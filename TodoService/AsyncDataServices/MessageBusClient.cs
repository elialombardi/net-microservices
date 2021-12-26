using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using TodoService.Dtos;

namespace TodoService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration configuration;
        private readonly IConnection connection;
        private readonly IModel channel;

        public MessageBusClient(IConfiguration configuration)
        {
            this.configuration = configuration;

            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMQHost"),
                Port = configuration.GetValue<int>("RabbitMQPort")
            };

            try
            {
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: configuration.GetValue<string>("RabbitMQExchange"), type: ExchangeType.Fanout);

                connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("RabbitMQ connection completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't connect to the message bus: {ex.Message}");
                throw;
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"RabbitMQ_ConnectionShutdown");
        }

        public void PublishTodoItem(TodoItemPublished todoItemPublished)
        {
            var message = JsonSerializer.Serialize(todoItemPublished);
            if (connection.IsOpen)
            {
                Console.WriteLine($"RabbitMQ: Sending message {message}");
                SendMessage(message);
            }
            else
                Console.WriteLine("RabbitMQ connection closed. Unable to send message");
        }
        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if (channel.IsOpen)
            {
                channel.Close();
                connection.Close();
            }
        }
        private void SendMessage(string message)
        {
            channel.BasicPublish(exchange: configuration.GetValue<string>("RabbitMQExchange"),
                routingKey: "",
                basicProperties: null,
                body: Encoding.UTF8.GetBytes(message));

            Console.WriteLine($"RabbitMQ: Message sent: {message}");
        }
    }
}