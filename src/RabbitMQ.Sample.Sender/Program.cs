using System;
using System.Text;
using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace RabbitMQ.Sample.Sender
{
    class Program
    {
        private static readonly Uri connectionString = new Uri("amqp://admin:RabbitMQ2019!@localhost:5672");
        private static readonly string queurName = "queue-sample";

        static void Main(string[] args)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            logger.Information("RabbitMQ Sender");

            try
            {
                var factory = new ConnectionFactory()
                {
                    Uri = connectionString
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: queurName,
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                while (true)
                {
                    channel.BasicPublish(exchange: "",
                                            routingKey: queurName,
                                            basicProperties: null,
                                            body: Encoding.UTF8.GetBytes("Jack =)"));

                    logger.Information("Message Sent");
                }

                // logger.Information("Sender Finished");
            }
            catch (Exception ex)
            {
                logger.Error($"Exception: {ex.GetType().FullName} | " + 
                                $"Message: {ex.Message}");
            }
        }
    }
}
