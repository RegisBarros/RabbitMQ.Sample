using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RabbitMQ.Sample.Consumer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*** Testing consumer message with RabbitMQ ***");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<RabbitMQConfiguration>(new RabbitMQConfiguration
                    {
                        ConnectionString = "amqp://admin:RabbitMQ2019!@localhost:5672",
                        Queue = "queue-sample"
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
