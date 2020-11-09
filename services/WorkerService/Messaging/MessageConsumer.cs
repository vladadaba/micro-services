using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace WorkerService.Messaging
{
    public class MessageConsumer : IHostedService
    {
        private readonly IConsumer<Ignore, string> consumer;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        public MessageConsumer()
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "kafka:9092",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
        }

        private void Subscribe()
        {
            consumer.Subscribe("test-topic");
        }

        private void StartConsuming(CancellationToken token)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        var cr = consumer.Consume(token);
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                Console.WriteLine("Consumer cancelled...");
                consumer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("UNEXPECTED EXCEPTIONS: " + ex.Message);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Subscribe();
            Task.Factory.StartNew(() => StartConsuming(cts.Token), TaskCreationOptions.LongRunning);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            cts.Cancel();

            return Task.CompletedTask;
        }
    }
}
