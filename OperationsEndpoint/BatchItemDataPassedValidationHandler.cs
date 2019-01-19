using System;
using System.Threading.Tasks;
using Messages.Operations.Events;
using Messages.Validation.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace OperationsEndpoint
{
    class BatchItemDataPassedValidationHandler : IHandleMessages<BatchItemDataPassedValidation>
    {
        public async Task Handle(BatchItemDataPassedValidation message, IMessageHandlerContext context)
        {
            Log.InfoFormat($"Performing Operation 2 for Batch Data Item {message.BatchDataItemId}");

            await Task.Delay(Random.Next(50, 150)).ConfigureAwait(false);

            await context.Publish(new Operation2Completed
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<BatchItemDataPassedValidationHandler>();
    }
}
