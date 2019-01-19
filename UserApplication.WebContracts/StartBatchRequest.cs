using System.Collections.Generic;

namespace UserApplication.WebContracts
{
    public class StartBatchRequest
    {
        public string BatchId { get; set; }
        public List<string> BatchData { get; set; }
    }
}
