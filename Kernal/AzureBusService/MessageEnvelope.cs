using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.AzureBusService;

public class MessageEnvelope<T>
{
    public T Payload { get; set; }
    public string MessageType { get; set; }
    public DateTime Timestamp { get; set; }

    public MessageEnvelope(T payload)
    {
        Payload = payload;
        MessageType = typeof(T).Name;
        Timestamp = DateTime.UtcNow;
    }
}
