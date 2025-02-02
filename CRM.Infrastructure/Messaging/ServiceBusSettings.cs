﻿namespace CRM.Infrastructure.Messaging;

public class ServiceBusSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string QueueName { get; set; } = "feedback-queue"; // Example queue for Feedback Service
}
