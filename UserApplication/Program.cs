using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using UserApplication.App;

static class Program
{
    static async Task Main()
    {
        Console.Title = "UserApplication";

        var batchSubmitter = new BatchSubmitter(ServerInfo.UserApplicationWebBackendBaseAddress);

        using (var hubConnection = new HubConnection(ServerInfo.UserApplicationWebBackendBaseAddress))
        {
            var hubProxy = hubConnection.CreateHubProxy("UserApplicationHub");

            hubProxy.On<string>("PushStatusUpdate", Console.WriteLine);

            await hubConnection.Start()
                .ConfigureAwait(false);

            Console.WriteLine("Press 'S' to send a batch request.");
            Console.WriteLine("Press the escape key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ConsoleKey.S)
                {
                    Console.WriteLine("Input a batch id and press enter.");
                    Console.Write("Batch id: ");
                    var batchId = Console.ReadLine().Replace("Batch id: ", "");

                    await batchSubmitter.Submit(batchId, BatchData.Generate()).ConfigureAwait(false);
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            hubConnection.Stop();
        }
    }
}