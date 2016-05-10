using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PostToEventHub
{
    public class UrlNotifier
    {
        public static void SetNotify(string url, TimeSpan interval)
        {
            System.Threading.ThreadPool.QueueUserWorkItem((obj) =>
            {
                while (true)
                {
                    try
                    {
                        Notify(url);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception notifying URL " + url + ": " + ex.Message);
                    }

                    System.Threading.Thread.Sleep(interval);
                }
            });
        }

        private static void Notify(string url)
        {
            if (url.Contains("{MY_IP}"))
            {
                var ipAddresses = GetLocalIPAddress();
                var myIp = ipAddresses.FirstOrDefault();
                if (!string.IsNullOrEmpty(Program.Options.IpPrefixes))
                {
                    foreach (var prefix in Program.Options.IpPrefixes.Split(';'))
                    {
                        string matchIp = ipAddresses.FirstOrDefault(ip => ip.StartsWith(prefix));
                        if (!string.IsNullOrEmpty(matchIp))
                        {
                            myIp = matchIp;
                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(myIp))
                {
                    Console.WriteLine("{MY_IP} could not be evaluated");
                }
                else
                {
                    Console.WriteLine("{MY_IP} evaluated to " + myIp);
                    url = url.Replace("{MY_IP}", myIp);
                }
            }

            using (HttpClient hc = new HttpClient())
            {
                Console.WriteLine("Calling " + url);
                hc.GetStringAsync(url).Wait();
            }
        }

        private static string[] GetLocalIPAddress()
        {
            List<string> ips = new List<string>();
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ip.ToString());
                }
            }

            return ips.ToArray();
        }
    }
}
