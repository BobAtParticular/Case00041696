using System.Threading.Tasks;
using Messages.BatchProcessing.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BatchProcessingEndpoint
{
    class BatchPostProcessingStage4CompletedHandler : IHandleMessages<BatchPostProcessingStage4Completed>
    {
        public Task Handle(BatchPostProcessingStage4Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Post processing (stage 5) starting for batch {message.BatchId}");
            return context.Publish(new BatchPostProcessingStage5Completed
            {
                BatchId = message.BatchId
            });
        }

        private static readonly ILog Log = LogManager.GetLogger<BatchPostProcessingStage4CompletedHandler>();
    }
}
