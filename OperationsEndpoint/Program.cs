﻿using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBusConfiguration;

class Program
{
    static async Task Main()
    {
        Console.Title = "OperationsEndpoint";
        var endpointConfiguration = new EndpointConfiguration("OperationsEndpoint");
        endpointConfiguration.ApplyCommonConfiguration();

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
            .ConfigureAwait(false);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}
