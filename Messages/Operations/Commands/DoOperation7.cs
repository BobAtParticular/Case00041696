using System;
using NServiceBus;

namespace Messages.Operations.Commands
{
    public class DoOperation7 : ICommand
    {
        public Guid BatchDataItemId { get; set; }
    }
}
