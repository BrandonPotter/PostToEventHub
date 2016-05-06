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

            try
            {
                var eventHubClient = EventHubClient.CreateFromConnectionString(Program.Options.EventHubConnectionString);
                eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(jsonString)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception sending event: " + ex.Message);
            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
