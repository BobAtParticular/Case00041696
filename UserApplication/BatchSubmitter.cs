using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserApplication.WebContracts;

namespace UserApplication.App
{
    class BatchSubmitter
    {
        private readonly HttpClient client;

        public BatchSubmitter(string apiServerBaseAddress)
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(apiServerBaseAddress)
            };
        }
        public Task Submit(string batchId, List<string> batchData)
        {
            var request = new StartBatchRequest()
            {
                BatchId = batchId,
                BatchData = batchData
            };

            var json = JsonConvert.SerializeObject(request);

            Console.WriteLine($"Submitting request for batch id {batchId} with {batchData.Count} items to {client.BaseAddress}/api/Batch");

            return client.PostAsync("/api/Batch", new StringContent(json, Encoding.UTF8, "application/json"));
        }
    }
}
