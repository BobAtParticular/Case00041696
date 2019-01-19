using NServiceBus;

namespace Messages.BatchProcessing.Events
{
    public class BatchPostProcessingStage4Completed : IEvent
    {
        public string BatchId { get; set; }
    }
}
