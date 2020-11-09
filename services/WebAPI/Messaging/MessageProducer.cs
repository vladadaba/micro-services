using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace WebAPI.Messaging
{
    public class MessageProducer
    {
        private readonly IProducer<Null, string> producer;

        public MessageProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "kafka:9092",
                Acks = Acks.All
            };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async void SendMessage(string message)
        {
            try
            {
                var dr = await producer.ProduceAsync("test-topic", new Message<Null, string> { Value = "test" });
                Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Delivery failed: {e.Error.Reason}");
            }
        }
    }
}
