using NServiceBus;

namespace Messages.BatchProcessing.Events
{
    public class BatchPostProcessingStage5Completed : IEvent
    {
        public string BatchId { get; set; }
    }
}