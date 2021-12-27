using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProjectService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProjectsService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly IEventProcessor eventProcessor;
        private IConnection connection;
        private IModel channel;
        private string queueName;

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            this.configuration = configuration;
            this.eventProcessor = eventProcessor;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetValue<string>("RabbitMQHost"),
                Port = configuration.GetValue<int>("RabbitMQPort"),
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            var exchange = configuration.GetValue<string>("RabbitMQExchange");
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);
            queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: exchange, routingKey: "");

            Console.WriteLine("---> Listening on the message bus");

            connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("---> Connection shutdown");
        }

        public override void Dispose()
        {
            if (channel.IsOpen)
                channel.Close();

            if (connection.IsOpen)
                connection.Close();

            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (ModuleHandle, ea) =>
            {
                Console.WriteLine($"---> Event Received");

                var body = ea.Body;
                var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                eventProcessor.ProcessEvent(notificationMessage);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}