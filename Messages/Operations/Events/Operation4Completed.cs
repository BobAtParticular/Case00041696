using System;
using NServiceBus;

namespace Messages.Operations.Events
{
    public class Operation4Completed : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}