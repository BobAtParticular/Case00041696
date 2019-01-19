using System;
using NServiceBus;

namespace Messages.Operations.Commands
{
    public class DoOperation4 : ICommand
    {
        public Guid BatchDataItemId { get; set; }
    }
}
