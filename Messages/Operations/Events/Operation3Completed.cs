using System;
using NServiceBus;

namespace Messages.Operations.Events
{
    public class Operation3Completed : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}
