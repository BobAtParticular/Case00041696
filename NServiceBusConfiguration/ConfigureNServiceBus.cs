using System;
using NServiceBus;

namespace NServiceBusConfiguration
{
    public static class ConfigureNServiceBus
    {
        public static TransportExtensions<LearningTransport> ApplyCommonConfiguration(this EndpointConfiguration endpointConfiguration)
        {
            endpointConfiguration.UsePersistence<LearningPersistence>();
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            var metrics = endpointConfiguration.EnableMetrics();

            metrics.SendMetricDataToServiceControl(
                serviceControlMetricsAddress: "Particular.Monitoring",
                interval: TimeSpan.FromSeconds(2)
            );

            endpointConfiguration.EnableInstallers();

            return transport;
        }
    }
}
