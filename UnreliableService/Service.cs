using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace UnreliableService
{
    public class Service
    {
        private CancellationToken token;
        private Random Random = new Random();

        public void Run(CancellationToken token)
        {
            this.token = token;
            Task.Run(InternalRun, token);
        }

        private async Task InternalRun()
        {
            while (!token.IsCancellationRequested)
            {
                using (WebApp.Start<OwinStartup>(ServerInfo.UnreliableServiceBaseAddress))
                {
                    var uptime = Random.Next(7000, 10000);
                    Console.WriteLine($"Unreliable Service is up for the next {uptime} milliseconds.");
                    await Task.Delay(uptime, token).ConfigureAwait(false);
                }

                var downtime = Random.Next(5000, 12000);
                Console.WriteLine($"Unreliable Service is down for the next {downtime} milliseconds.");
                await Task.Delay(downtime, token).ConfigureAwait(false);
            }
        }
    }
}
