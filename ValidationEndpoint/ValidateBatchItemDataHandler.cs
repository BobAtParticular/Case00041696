using System;
using System.Threading.Tasks;
using Messages.Validation.Commands;
using Messages.Validation.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace ValidationEndpoint
{
    class ValidateBatchItemDataHandler : IHandleMessages<ValidateBatchItemData>
    {
        public Task Handle(ValidateBatchItemData message, IMessageHandlerContext context)
        {
            var validationScore = Random.NextDouble();

            Log.InfoFormat($"Batch data item {message.BatchDataItemId} validation was scored at {validationScore}");

            return validationScore < .15 ?
                context.Publish(new BatchItemDataFailedValidation
                {
                    BatchDataItemId = message.BatchDataItemId,
                    Score = validationScore
                }) :
                context.Publish(new BatchItemDataPassedValidation
                {
                    BatchDataItemId = message.BatchDataItemId
                });
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<ValidateBatchItemDataHandler>();
    }
}
