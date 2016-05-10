using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ServiceBus.Messaging;

namespace PostToEventHub.Controllers
{
    public class EventsController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Post(HttpRequestMessage request)
        {
            var jsonString = await request.Content.ReadAsStringAsync();

            // Do something with the string
            Console.WriteLine("RX: " + jsonString);
            EventHubPoster.PostEventData(jsonString);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
