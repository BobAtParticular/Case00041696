using System.IO;
using System.Web.Http;
using Messages.BatchProcessing.Commands;
using NServiceBus;
using NServiceBus.Logging;
using UserApplication.WebContracts;

namespace UserApplication.WebBackend
{
    public class BatchController : ApiController
    {
        public static IMessageSession MessageSession { get; set; }

        public async void Post([FromBody] StartBatchRequest request)
        {
            Log.Info($"Processing send batch request for batch id {request.BatchId} with {request.BatchData.Count} items.");
            var batchDataFilePath = Path.GetTempFileName();

            File.AppendAllLines(batchDataFilePath, request.BatchData);

            await MessageSession.Send(new StartBatch
            {
                BatchId = request.BatchId,
                BatchDataPath = batchDataFilePath
            });
        }

        private static readonly ILog Log = LogManager.GetLogger<BatchController>();
    }
}
