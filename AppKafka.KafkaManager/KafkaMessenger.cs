using AppKafka.Core.Domain;
using AppKafka.Core.ServiceContract;
using Confluent.Kafka;
using Newtonsoft.Json;
using System;
using System.Threading;


namespace AppKafka.KafkaManager
{
    public class KafkaMessenger : IKafkaMessenger<string>
    {
        public string GetTopicName => "testeapp-topic";
        public string GetServerLocation => "localhost:9092";

        public void MessageConsumer(Action<string> doSomething)
        {
            var config = new ConsumerConfig
            {
                GroupId = "testeapp-consumers",
                BootstrapServers = this.GetServerLocation,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(this.GetTopicName);

                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume(cts.Token);
                            doSomething(cr.Value);
                        }
                        catch (ConsumeException ex)
                        {
                            throw ex;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    consumer.Close();
                }
            }
        }

        public async void MessageProducer(KafkaMessage message)
        {
            var config = new ProducerConfig { BootstrapServers = this.GetServerLocation };
            var producerBuilder = new ProducerBuilder<Null, string>(config);
            var _message = JsonConvert.SerializeObject(message);
            
            using (var producer = producerBuilder.Build())
            {
                var result = await producer
                    .ProduceAsync(topic: this.GetTopicName, new Message<Null, string>()
                        {
                            Value = _message,
                            Timestamp = new Timestamp(DateTime.Now)
                        }
                    )
                    .ConfigureAwait(false);
            }
        }
    }
}
