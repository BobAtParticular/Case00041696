using NServiceBus;

namespace Messages.BatchProcessing.Events
{
    public class BatchStarted : IEvent
    {
        public string BatchId { get; set; }
        public string BatchDataPath { get; set; }
        public int BatchItemDataCount { get; set; }
    }
}
