using AppKafka.Core.ServiceContract;
using AppKafka.KafkaManager;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace AppKafa.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<IKafkaMessenger<string>>(new KafkaMessenger());
            var serviceProvider = services.BuildServiceProvider();
            var appKafka = serviceProvider.GetService<IKafkaMessenger<string>>();

            Action<string> val = delegate (string str)
            {
                Console.WriteLine("  ");
                Console.WriteLine(str);
                Console.WriteLine("  ");
            };

            while (true)
            {
                appKafka.MessageConsumer(val);
            }
        }
    }
}
