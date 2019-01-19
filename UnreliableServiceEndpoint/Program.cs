using System;
using System.Net.Http;
using System.Threading.Tasks;
using Messages;
using NServiceBus;
using NServiceBusConfiguration;

class Program
{
    static async Task Main()
    {
        Console.Title = "UnreliableServiceEndpoint";
        var endpointConfiguration = new EndpointConfiguration("UnreliableServiceEndpoint");

        endpointConfiguration.ApplyCommonConfiguration().RegisterRoutes();

        endpointConfiguration.Recoverability()
            .Immediate(retries => retries.NumberOfRetries(1))
            .Delayed(delayed =>
                delayed.NumberOfRetries(0)); //allows you to see error queue operation with ServicePulse

        var client = new HttpClient
        {
            BaseAddress = new Uri(ServerInfo.UnreliableServiceBaseAddress),
            Timeout = TimeSpan.FromSeconds(5)
        };

        endpointConfiguration.RegisterComponents(c => c.RegisterSingleton(client));

        endpointConfiguration.ReportCustomChecksTo("Particular.ServiceControl");

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}
