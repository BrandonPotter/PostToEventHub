using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace PostToEventHub
{
    class Program
    {
        internal static Options Options { get; set; }
        static void Main(string[] args)
        {
            Options opts = new Options();
            var result = CommandLine.Parser.Default.ParseArguments(args, opts);
            Program.Options = opts;

            Console.WriteLine("Hub = " + opts.EventHubConnectionString);
            Console.WriteLine("Port = " + opts.Port.ToString());

            string baseAddress = "http://*:" + opts.Port.ToString() + "/";
            try
            {
                WebApp.Start<Startup>(url: baseAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception starting server on " + baseAddress + ": " + ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
