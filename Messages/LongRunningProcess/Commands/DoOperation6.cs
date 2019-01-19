using System;
using NServiceBus;

namespace Messages.LongRunningProcess.Commands
{
    public class DoOperation6 : ICommand
    {
        public Guid BatchDataItemId { get; set; }
    }
}
