using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.Interfaces;

public interface IMessageSubscriber
{
    Task SubscribeAsync<T>(string queueName, Func<T, Task> messageHandler);
}


