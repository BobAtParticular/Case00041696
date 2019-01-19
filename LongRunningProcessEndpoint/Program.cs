using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBusConfiguration;

static class Program
{
    static async Task Main()
    {
        Console.Title = "LongRunningProcessEndpoint";
        var endpointConfiguration = new EndpointConfiguration("LongRunningProcessEndpoint");
        endpointConfiguration.ApplyCommonConfiguration();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}