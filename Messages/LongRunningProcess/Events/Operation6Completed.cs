using System;
using NServiceBus;

namespace Messages.LongRunningProcess.Events
{
    public class Operation6Completed : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}