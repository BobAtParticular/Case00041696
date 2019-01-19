using System;
using System.Threading.Tasks;
using Messages.Operations.Commands;
using Messages.Operations.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace OperationsEndpoint
{
    class DoOperation4Handler : IHandleMessages<DoOperation4>
    {
        public async Task Handle(DoOperation4 message, IMessageHandlerContext context)
        {
            Log.Info($"Doing operation 4 for batch data item {message.BatchDataItemId}.");
            await Task.Delay(Random.Next(20, 120)).ConfigureAwait(false);
            await context.Publish(new Operation4Completed
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<DoOperation4Handler>();
    }
}
