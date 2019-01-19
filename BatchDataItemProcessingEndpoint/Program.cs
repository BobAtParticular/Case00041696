using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBusConfiguration;

class Program
{
    static async Task Main()
    {
        Console.Title = "BatchDataItemProcessingEndpoint";
        var endpointConfiguration = new EndpointConfiguration("BatchDataItemProcessingEndpoint");

        endpointConfiguration.ApplyCommonConfiguration().RegisterRoutes();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}
