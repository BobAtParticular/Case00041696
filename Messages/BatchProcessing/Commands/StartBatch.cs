using NServiceBus;

namespace Messages.BatchProcessing.Commands
{
    public class StartBatch : ICommand
    {
        public string BatchId { get; set; }
        public string BatchDataPath { get; set; }
    }
}
