using System.Threading.Tasks;
using Messages.BatchDataItemProcessing.Events;
using Messages.BatchProcessing.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BatchProcessingEndpoint
{
    public class BatchSaga : Saga<BatchSagaData>,
        IAmStartedByMessages<BatchStarted>,
        IHandleMessages<BatchItemDataSagaComplete>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<BatchSagaData> mapper)
        {
            mapper.ConfigureMapping<BatchStarted>(message => message.BatchId).ToSaga(saga => saga.BatchId);
            mapper.ConfigureMapping<BatchItemDataSagaComplete>(message => message.BatchId).ToSaga(saga => saga.BatchId);
        }

        public Task Handle(BatchStarted message, IMessageHandlerContext context)
        {
            Log.Info($"Starting BatchSaga for batch id {message.BatchId}");
            Data.BatchId = message.BatchId;
            Data.BatchItemDataCount = message.BatchItemDataCount;
            Data.BatchDataItemsRemaining = message.BatchItemDataCount;

            return Task.CompletedTask;
        }

        public Task Handle(BatchItemDataSagaComplete message, IMessageHandlerContext context)
        {
            Data.BatchDataItemsRemaining--;

            Log.Info($"{Data.BatchDataItemsRemaining} / {Data.BatchItemDataCount} remaining for BatchId {Data.BatchId}");

            if (Data.BatchDataItemsRemaining > 0)
            {
                return context.Publish(new BatchProgressUpdated
                {
                    BatchId = Data.BatchId,
                    RemainingItems = Data.BatchDataItemsRemaining,
                    TotalItems = Data.BatchItemDataCount
                });
            }

            MarkAsComplete();
            return context.Publish(new BatchCompleted {BatchId = Data.BatchId});
        }

        private static readonly ILog Log = LogManager.GetLogger<BatchSaga>();
    }
}
