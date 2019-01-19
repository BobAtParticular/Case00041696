using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Messages.Operations.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace UnreliableServiceEndpoint
{
    class Operation2CompletedHandler : IHandleMessages<Operation2Completed>
    {
        private readonly HttpClient client;

        public Operation2CompletedHandler(HttpClient client)
        {
            this.client = client;
        }

        public async Task Handle(Operation2Completed message, IMessageHandlerContext context)
        {
            Log.Info($"Starting transient failure prone operation 3 for batch data item {message.BatchDataItemId}");

            var response = await client.PostAsync("/api/Service", new StringContent(message.BatchDataItemId.ToString()));

            await context.Publish(new Operation3Completed
            {
                BatchDataItemId = message.BatchDataItemId
            }).ConfigureAwait(false);
        }

        private static readonly Random Random = new Random();
        private static readonly ILog Log = LogManager.GetLogger<Operation2CompletedHandler>();
    }
}
