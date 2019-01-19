using NServiceBus;

namespace Messages.BatchProcessing.Events
{
    public class BatchProgressUpdated : IEvent
    {
        public string BatchId { get; set; }
        public int RemainingItems { get; set; }
        public int TotalItems { get; set; }
    }
}
