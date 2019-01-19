using NServiceBus;

namespace Messages.BatchProcessing.Events
{
    public class BatchCompleted : IEvent
    {
        public string BatchId { get; set; }
    }
}
