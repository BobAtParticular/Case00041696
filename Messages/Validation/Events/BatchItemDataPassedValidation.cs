using System;
using NServiceBus;

namespace Messages.Validation.Events
{
    public class BatchItemDataPassedValidation : IEvent
    {
        public Guid BatchDataItemId { get; set; }
    }
}
