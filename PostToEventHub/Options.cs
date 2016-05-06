using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace PostToEventHub
{
    class Options
    {
        [Option('h', "hub", Required = true,
          HelpText = "Event Hub connection string.")]
        public string EventHubConnectionString { get; set; }

        [Option('p', "port", Required = true,
            HelpText = "Port to listen on")]
        public int Port { get; set; }
    }
}
