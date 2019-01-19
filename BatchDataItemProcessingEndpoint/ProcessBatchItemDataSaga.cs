using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages.BatchDataItemProcessing.Commands;
using Messages.BatchDataItemProcessing.Events;
using Messages.LongRunningProcess.Commands;
using Messages.LongRunningProcess.Events;
using Messages.Operations.Commands;
using Messages.Operations.Events;
using Messages.Validation.Commands;
using Messages.Validation.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BatchDataItemProcessingEndpoint
{
    public class ProcessBatchItemDataSaga : Saga<ProcessBatchItemDataSagaData>,
        IAmStartedByMessages<ProcessBatchItemData>,
        IHandleMessages<BatchItemDataFailedValidation>,
        IHandleMessages<Operation3Completed>,
        IHandleMessages<Operation4Completed>,
        IHandleMessages<Operation6Completed>,
        IHandleMessages<Operation7Completed>,
        IHandleMessages<Operation8Completed>,
        IHandleTimeouts<ProcessBatchItemDataSaga.ParallelTasksAreTakingTooLong>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ProcessBatchItemDataSagaData> mapper)
        {
            mapper.ConfigureMapping<ProcessBatchItemData>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
            mapper.ConfigureMapping<BatchItemDataFailedValidation>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
            mapper.ConfigureMapping<Operation3Completed>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
            mapper.ConfigureMapping<Operation4Completed>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
            mapper.ConfigureMapping<Operation6Completed>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
            mapper.ConfigureMapping<Operation7Completed>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
            mapper.ConfigureMapping<Operation8Completed>(message => message.BatchDataItemId)
                .ToSaga(saga => saga.BatchDataItemId);
        }

        public async Task Handle(ProcessBatchItemData message, IMessageHandlerContext context)
        {
            Log.Info($"Starting ProcessBatchItemDataSaga for data item id {message.BatchDataItemId} in batch {message.BatchId}");
            Data.BatchId = message.BatchId;
            Data.BatchDataItemId = message.BatchDataItemId;

            await context.Send(new ValidateBatchItemData
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);

            await context.Publish(new ProcessBatchItemDataSagaStarted
            {
                BatchId = message.BatchId,
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private Task SagaCompleted(IMessageHandlerContext context)
        {
            Log.Info($"ProcessBatchItemDataSaga for data item id {Data.BatchDataItemId} in batch {Data.BatchId} is complete.");

            MarkAsComplete();

            return context.Publish(new BatchItemDataSagaComplete
            {
                BatchId = Data.BatchId,
                BatchDataItemId = Data.BatchDataItemId
            });
        }

        public Task Handle(BatchItemDataFailedValidation message, IMessageHandlerContext context)
        {
            Log.Info($"ProcessBatchItemDataSaga for data item id {Data.BatchDataItemId} in batch {Data.BatchId} detected failed validation");
            return SagaCompleted(context);
        }

        public Task Handle(Operation3Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Operation 3 completed for data item id {Data.BatchDataItemId} in batch {Data.BatchId}. Launching parallel tasks.");
            return Task.WhenAll(new List<Task>
            {
                RequestTimeout<ParallelTasksAreTakingTooLong>(context, TimeSpan.FromMilliseconds(8000)),
                context.Send(new DoOperation4 {BatchDataItemId = Data.BatchDataItemId}),
                context.Send(new DoOperation6 {BatchDataItemId = Data.BatchDataItemId}),
                context.Send(new DoOperation7 {BatchDataItemId = Data.BatchDataItemId}),
                context.Send(new DoOperation8 {BatchDataItemId = Data.BatchDataItemId})
            });
        }

        private Task CheckIfOperations467and8AreComplete(IMessageHandlerContext context)
        {
            var operationsStatus = new Dictionary<string,bool>
            {
                {"Operation4", Data.Operation4Complete},
                {"Operation6", Data.Operation4Complete},
                {"Operation7", Data.Operation4Complete},
                {"Operation8", Data.Operation4Complete}
            };
            if (operationsStatus.Values.All(v => v))
            {
                Log.Info($"Parallel operations completed for data item id {Data.BatchDataItemId} in batch {Data.BatchId}");
                return SagaCompleted(context);
            }

            Log.Info($"Waiting for {string.Join(", ", operationsStatus.Where(kvp => !kvp.Value).Select(kvp => kvp.Key))} to complete for data item id {Data.BatchDataItemId} in batch {Data.BatchId}.");

            return Task.CompletedTask;
        }

        public Task Handle(Operation4Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Operation 4 completed for data item id {Data.BatchDataItemId} in batch {Data.BatchId}");
            Data.Operation4Complete = true;
            return CheckIfOperations467and8AreComplete(context);
        }

        public Task Handle(Operation6Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Operation 6 completed for data item id {Data.BatchDataItemId} in batch {Data.BatchId}");
            Data.Operation6Complete = true;
            return CheckIfOperations467and8AreComplete(context);
        }

        public Task Handle(Operation7Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Operation 7 completed for data item id {Data.BatchDataItemId} in batch {Data.BatchId}");
            Data.Operation7Complete = true;
            return CheckIfOperations467and8AreComplete(context);
        }

        public Task Handle(Operation8Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Operation 8 completed for data item id {Data.BatchDataItemId} in batch {Data.BatchId}");
            Data.Operation8Complete = true;
            return CheckIfOperations467and8AreComplete(context);
        }

        public class ParallelTasksAreTakingTooLong
        {

        }

        public Task Timeout(ParallelTasksAreTakingTooLong state, IMessageHandlerContext context)
        {
            Log.Info($"Parallel operations timed out after 16 seconds for data item id {Data.BatchDataItemId} in batch {Data.BatchId}");
            return context.Publish(new BatchItemDataItemTimedOut
            {
                BatchId = Data.BatchId,
                BatchDataItemId = Data.BatchDataItemId
            });
        }

        private static readonly ILog Log = LogManager.GetLogger<ProcessBatchItemDataSaga>();
    }
}
