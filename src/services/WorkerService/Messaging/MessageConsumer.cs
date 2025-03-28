﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WorkerService.Options;

namespace WorkerService.Messaging
{
    public class MessageConsumer : IHostedService
    {
        private readonly IConsumer<Ignore, string> consumer;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();
        private readonly IOptions<KafkaOptions> _options;

        public MessageConsumer(IOptions<KafkaOptions> options)
        {
            _options = options;
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = _options.Value.BootstrapServers,
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
            consumer.Subscribe(_options.Value.JobRequestsTopic);
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
                        cr.Message.Headers.TryGetLastBytes("correlation_id", out var bytes);
                        var correlationId = bytes == null ? "correlation_id_is_null" : Encoding.UTF8.GetString(bytes);
                        Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                        Console.WriteLine($"CorrelationId={correlationId}");
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
