using System;
using System.Threading;
using UnreliableService;

static class Program
{
    static void Main()
    {
        Console.Title = "UnreliableService";

        var tokenSource = new CancellationTokenSource();

        var service = new Service();

        service.Run(tokenSource.Token);

        Console.WriteLine("Press any key to exit");
        Console.ReadKey(true);

        tokenSource.Cancel();
    }
}
