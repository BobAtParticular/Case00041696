using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Messages.BatchDataItemProcessing.Commands;
using Messages.BatchProcessing.Commands;
using Messages.BatchProcessing.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace BatchProcessingEndpoint
{
    public class StartBatchHandler : IHandleMessages<StartBatch>
    {
        public async Task Handle(StartBatch message, IMessageHandlerContext context)
        {
            Log.Info($"Received StartBatch request for BatchId {message.BatchId}");
            var batchData = await File.ReadAllLinesAsync(message.BatchDataPath).ConfigureAwait(false);

            var dispatches = new List<Task>();

            foreach (var data in batchData)
            {
                var batchDataItemId = Guid.Parse(data);
                dispatches.Add(context.Send(new ProcessBatchItemData
                {
                    BatchDataItemId = batchDataItemId,
                    BatchId = message.BatchId
                }));
            }

            dispatches.Add(context.Publish(new BatchStarted
            {
                BatchId = message.BatchId,
                BatchDataPath = message.BatchDataPath,
                BatchItemDataCount = batchData.Length
            }));

            await Task.WhenAll(dispatches).ConfigureAwait(false);
        }

        private static ILog Log = LogManager.GetLogger<StartBatchHandler>();
    }
}
