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
        [Option('h', "hub", Required = false,
          HelpText = "Event Hub connection string.")]
        public string EventHubConnectionString { get; set; }

        [Option("hubname", Required = false,
            HelpText = "Event Hub Name")]
        public string EventHubName { get; set; }

        [Option('p', "port", Required = true,
            HelpText = "Port to listen on")]
        public int Port { get; set; }

        [Option('i', "ipprefix", Required = false,
            HelpText = "IP prefixes preference")]
        public string IpPrefixes { get; set; }

        [Option('u', "notifyurl", Required = false,
            HelpText = "Notify URL (will be called every minute)")]
        public string NotifyUrl { get; set; }

        [Option('s', "signalrurl", Required = false,
            HelpText = "SignalR Connection URL (for SignalR event pull)")]
        public string SignalRURL { get; set; }

        [Option('e', "signalrevent", Required = false,
            HelpText = "SignalR Event Name")]
        public string SignalREvent { get; set; }

        [Option('n', "signalrhubname", Required = false,
            HelpText = "SignalR Hub Name")]
        public string SignalRHubName { get; set; }

        [Option("sqlserverconnstring", Required = false,
            HelpText = "SQL Server Connection String")]
        public string SqlServerConnectionString { get; set; }

        [Option("sqlpostquery", Required = false,
            HelpText = "SQL POST query")]
        public string SqlPostQuery { get; set; }

        [Option("postgresqlconnectionstring", Required = false,
            HelpText = "PostgreSQL Connection String")]
        public string PostgreSqlServerConnectionString { get; set; }

        [Option("postgresqlpostquery", Required = false,
            HelpText = "PostgreSQL POST query")]
        public string PostgreSqlPostQuery { get; set; }
    }
}
