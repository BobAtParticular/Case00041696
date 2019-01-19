using System.Threading.Tasks;
using Messages.BatchProcessing.Events;
using Microsoft.AspNet.SignalR;
using NServiceBus;
using NServiceBus.Logging;

namespace UserApplication.WebBackend
{
    public class BatchEventsHandlers :
        IHandleMessages<BatchStarted>,
        IHandleMessages<BatchProgressUpdated>,
        IHandleMessages<BatchCompleted>,
        IHandleMessages<BatchPostProcessingStage4Completed>,
        IHandleMessages<BatchPostProcessingStage5Completed>
    {
        public Task Handle(BatchStarted message, IMessageHandlerContext context)
        {
            var statusMessage = $"Batch with id {message.BatchId} started with {message.BatchItemDataCount} data items.";
            return PushStatusUpdate(statusMessage);
        }

        public Task Handle(BatchProgressUpdated message, IMessageHandlerContext context)
        {
            var statusMessage = $"Batch with id {message.BatchId} status changed: {message.RemainingItems} / {message.TotalItems} data items remaining to process.";
            return PushStatusUpdate(statusMessage);
        }

        public Task Handle(BatchCompleted message, IMessageHandlerContext context)
        {
            var statusMessage = $"Batch with id {message.BatchId} completed.";
            return PushStatusUpdate(statusMessage);
        }

        public Task Handle(BatchPostProcessingStage4Completed message, IMessageHandlerContext context)
        {
            var statusMessage = $"Batch with id {message.BatchId} post processing stage 4 completed.";
            return PushStatusUpdate(statusMessage);
        }

        public Task Handle(BatchPostProcessingStage5Completed message, IMessageHandlerContext context)
        {
            var statusMessage = $"Batch with id {message.BatchId} post processing stage 5 completed.";
            return PushStatusUpdate(statusMessage);
        }

        private Task PushStatusUpdate(string statusMessage)
        {
            Log.Info($"Emitting status update '{statusMessage}'");
            return Hub.Clients.All.PushStatusUpdate(statusMessage);
        }

        private static readonly IHubContext Hub = GlobalHost.ConnectionManager.GetHubContext<UserApplicationHub>();
        private static readonly ILog Log = LogManager.GetLogger<BatchEventsHandlers>();
    }
}
