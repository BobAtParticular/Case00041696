using System;
using System.Threading.Tasks;
using Messages.Operations.Commands;
using Messages.Operations.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace OperationsEndpoint
{
    class DoOperation8Handler : IHandleMessages<DoOperation8>
    {
        public async Task Handle(DoOperation8 message, IMessageHandlerContext context)
        {
            Log.Info($"Doing operation 8 for batch data item {message.BatchDataItemId}.");
            await Task.Delay(Random.Next(80, 120)).ConfigureAwait(false);
            await context.Publish(new Operation8Completed
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<DoOperation8Handler>();
    }
}
