using System;
using NServiceBus;

namespace Messages.Validation.Events
{
    public class BatchItemDataFailedValidation : IEvent
    {
        public Guid BatchDataItemId { get; set; }
        public double Score { get; set; }
    }
}