﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernal.Interfaces;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message);
}

