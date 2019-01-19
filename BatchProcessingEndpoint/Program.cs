using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBusConfiguration;

static class Program
{
    static async Task Main()
    {
        Console.Title = "BatchProcessingEndpoint";
        var endpointConfiguration = new EndpointConfiguration("BatchProcessingEndpoint");

        endpointConfiguration.ApplyCommonConfiguration().RegisterRoutes();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}