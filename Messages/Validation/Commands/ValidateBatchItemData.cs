using System;
using NServiceBus;

namespace Messages.Validation.Commands
{
    public class ValidateBatchItemData : ICommand
    {
        public Guid BatchDataItemId { get; set; }
    }
}
