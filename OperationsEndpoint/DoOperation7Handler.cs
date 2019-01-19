using System;
using System.Threading.Tasks;
using Messages.Operations.Commands;
using Messages.Operations.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace OperationsEndpoint
{
    class DoOperation7Handler : IHandleMessages<DoOperation7>
    {
        public async Task Handle(DoOperation7 message, IMessageHandlerContext context)
        {
            Log.Info($"Doing operation 7 for batch data item {message.BatchDataItemId}.");
            await Task.Delay(Random.Next(300, 400)).ConfigureAwait(false);
            await context.Publish(new Operation7Completed
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<DoOperation7Handler>();
    }
}
