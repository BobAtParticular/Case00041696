using System;
using NServiceBus;

namespace BatchDataItemProcessingEndpoint
{
    public class ProcessBatchItemDataSagaData : ContainSagaData
    {
        public string BatchId { get; set; }
        public Guid BatchDataItemId { get; set; }
        public bool Operation4Complete { get; set; }
        public bool Operation6Complete { get; set; }
        public bool Operation7Complete { get; set; }
        public bool Operation8Complete { get; set; }
    }
}