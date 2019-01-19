using NServiceBus;

namespace BatchProcessingEndpoint
{
    public class BatchSagaData : ContainSagaData
    {
        public string BatchId { get; set; }
        public int BatchItemDataCount { get; set; }
        public int BatchDataItemsRemaining { get; set; }
    }
}