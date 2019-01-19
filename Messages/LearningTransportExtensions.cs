using Messages.BatchDataItemProcessing.Commands;
using Messages.BatchProcessing.Commands;
using Messages.LongRunningProcess.Commands;
using Messages.Operations.Commands;
using Messages.Validation.Commands;
using NServiceBus;

namespace Messages
{
    public static class LearningTransportExtensions
    {
        public static void RegisterRoutes(this TransportExtensions<LearningTransport> transport)
        {
            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(StartBatch).Assembly, typeof(StartBatch).Namespace, "BatchProcessingEndpoint");
            routing.RouteToEndpoint(typeof(ProcessBatchItemData), "BatchDataItemProcessingEndpoint");
            routing.RouteToEndpoint(typeof(DoOperation6), "LongRunningProcessEndpoint");
            routing.RouteToEndpoint(typeof(DoOperation4).Assembly, typeof(DoOperation4).Namespace, "OperationsEndpoint");
            routing.RouteToEndpoint(typeof(ValidateBatchItemData), "ValidationEndpoint");
        }
    }
}
