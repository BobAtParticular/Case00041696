using System;
using System.Web.Http;

namespace UnreliableService
{
    public class ServiceController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }

        public void Post([FromBody] Guid batchDataItemId)
        {
            Console.WriteLine($"POST for Batch data item id {batchDataItemId} accepted");
        }
    }
}
