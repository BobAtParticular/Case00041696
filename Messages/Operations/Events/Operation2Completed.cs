using System;
using NServiceBus;

namespace Messages.Operations.Events
{
    public class Operation2Completed : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}
