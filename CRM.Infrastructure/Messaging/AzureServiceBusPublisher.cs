using Azure.Messaging.ServiceBus;
using Kernal.Interfaces;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CRM.Infrastructure.Messaging;
public class AzureServiceBusPublisher : IMessagePublisher
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly string _queueName;

    public AzureServiceBusPublisher(IOptions<ServiceBusSettings> serviceBusSettings)
    {
        var connectionString = serviceBusSettings.Value.ConnectionString;
        _queueName = serviceBusSettings.Value.QueueName;
        _serviceBusClient = new ServiceBusClient(connectionString);
    }

    public async Task PublishAsync<T>(T message)
    {
        var sender = _serviceBusClient.CreateSender(_queueName);
        var messageBody = JsonSerializer.Serialize(message);
        var serviceBusMessage = new ServiceBusMessage(messageBody);

        await sender.SendMessageAsync(serviceBusMessage);
    }
}

