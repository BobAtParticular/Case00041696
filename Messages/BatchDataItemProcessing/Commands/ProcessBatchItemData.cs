using System;
using NServiceBus;

namespace Messages.BatchDataItemProcessing.Commands
{
    public class ProcessBatchItemData : ICommand
    {
        public string BatchId { get; set; }
        public Guid BatchDataItemId { get; set; }
    }
}
