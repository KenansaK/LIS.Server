using Azure.Messaging.ServiceBus;
using Kernal.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CRM.Infrastructure.Messaging;
public class AzureServiceBusSubscriber : IMessageSubscriber
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly string _queueName;

    public AzureServiceBusSubscriber(IOptions<ServiceBusSettings> serviceBusSettings)
    {
        var connectionString = serviceBusSettings.Value.ConnectionString;
        _queueName = serviceBusSettings.Value.QueueName;
        _serviceBusClient = new ServiceBusClient(connectionString);
    }

    public async Task SubscribeAsync<T>(string queueName, Func<T, Task> messageHandler)
    {
        var processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());

        processor.ProcessMessageAsync += async args =>
        {
            var message = JsonSerializer.Deserialize<T>(args.Message.Body.ToString());
            await messageHandler(message);
            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine($"Error: {args.Exception.Message}");
            return Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
    }
}
