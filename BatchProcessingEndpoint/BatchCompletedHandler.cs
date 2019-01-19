using System.Threading.Tasks;
using Messages.BatchProcessing.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BatchProcessingEndpoint
{
    public class BatchCompletedHandler : IHandleMessages<BatchCompleted>
    {
        public Task Handle(BatchCompleted message, IMessageHandlerContext context)
        {
            Log.Info($"Post processing (stage 4) starting for batch {message.BatchId}");
            return context.Publish(new BatchPostProcessingStage4Completed
            {
                BatchId = message.BatchId
            });
        }

        private static readonly ILog Log = LogManager.GetLogger<BatchStartedHandler>();
    }
}
