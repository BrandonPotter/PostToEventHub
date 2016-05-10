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

        [Option('i', "ipprefix", Required = false,
            HelpText = "IP prefixes preference")]
        public string IpPrefixes { get; set; }

        [Option('u', "notifyurl", Required = false,
            HelpText = "Notify URL (will be called every minute)")]
        public string NotifyUrl { get; set; }
    }
}
