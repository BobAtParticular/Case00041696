using System;
using NServiceBus;

namespace Messages.Operations.Events
{
    public class Operation7Completed : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}