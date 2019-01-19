using System.IO;
using System.Threading.Tasks;
using Messages.BatchProcessing.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BatchProcessingEndpoint
{
    public class BatchStartedHandler : IHandleMessages<BatchStarted>
    {
        public Task Handle(BatchStarted message, IMessageHandlerContext context)
        {
            Log.Info($"Batch data deleted at {message.BatchDataPath} for Batch Id {message.BatchId}.");
            File.Delete(message.BatchDataPath);
            return Task.CompletedTask;
        }

        private static ILog Log = LogManager.GetLogger<BatchStartedHandler>();
    }
}
