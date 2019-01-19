using System;
using NServiceBus;

namespace Messages.Operations.Commands
{
    public class DoOperation8 : ICommand
    {
        public Guid BatchDataItemId { get; set; }
    }
}