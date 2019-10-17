using AppKafka.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppKafka.Core.ServiceContract
{
    public interface IKafkaMessenger<T>
    {
        string GetTopicName{ get; }
        string GetServerLocation { get; }
        void MessageProducer(KafkaMessage message);
        void MessageConsumer(Action<T> doSomething);
    }
}
