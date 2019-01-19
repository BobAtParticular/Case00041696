using System;
using System.Threading.Tasks;
using Messages.LongRunningProcess.Commands;
using Messages.LongRunningProcess.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace LongRunningProcessEndpoint
{
    class DoOperation6Handler : IHandleMessages<DoOperation6>
    {
        public async Task Handle(DoOperation6 message, IMessageHandlerContext context)
        {
            var delay = Random.Next(5000, 10000);

            Log.Info($"Performing Operation 6 for batch data item {message.BatchDataItemId}. This will take {delay} milliseconds to complete.");

            await Task.Delay(delay).ConfigureAwait(false);

            await context.Publish(new Operation6Completed
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<DoOperation6Handler>();
    }
}
