using System;
using NServiceBus;

namespace Messages.Operations.Events
{
    public class Operation8Completed : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}