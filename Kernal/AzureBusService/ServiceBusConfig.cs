using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.AzureBusService;

public class ServiceBusConfig
{
    public string ConnectionString { get; set; }
    public string QueueName { get; set; }
    public string TopicName { get; set; }
}
