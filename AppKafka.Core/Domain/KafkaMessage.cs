using System;

namespace AppKafka.Core.Domain
{
    public class KafkaMessage
    {
        public Guid Id { get; set; }
        public string MessageValue { get; set; }
    }
}
