using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM.Shared.Interfaces;

public interface IMessagePublisher
{
    void Publish<TEventEnum, TMessage>(TEventEnum eventEnum, TMessage message);
}