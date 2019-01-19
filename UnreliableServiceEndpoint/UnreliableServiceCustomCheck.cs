using System;
using System.Net.Http;
using System.Threading.Tasks;
using NServiceBus.CustomChecks;

namespace UnreliableServiceEndpoint
{
    class UnreliableServiceCustomCheck : ICustomCheck
    {
        private readonly HttpClient client;

        public UnreliableServiceCustomCheck(HttpClient client)
        {
            this.client = client;
        }

        public string Category => "UnreliableService";

        public string Id => "CheckStatus";

        public TimeSpan? Interval => TimeSpan.FromSeconds(1);

        public async Task<CheckResult> PerformCheck()
        {
            try
            {
                await client.GetAsync("/api/Service").ConfigureAwait(false);
                return CheckResult.Pass;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Check failed: {e.Message}");
                return CheckResult.Failed(e.Message);
            }
        }
    }
}
