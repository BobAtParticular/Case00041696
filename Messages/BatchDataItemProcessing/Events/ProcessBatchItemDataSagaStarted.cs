﻿using System;
using NServiceBus;

namespace Messages.BatchDataItemProcessing.Events
{
    public class ProcessBatchItemDataSagaStarted : IEvent
    {
        public string BatchId { get; set; }
        public Guid BatchDataItemId { get; set; }
    }
}
