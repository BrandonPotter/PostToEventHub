using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace PostToEventHub
{
    public class SignalRClient
    {
        private static HubConnection _hc = null;
        public static void SetSignalR(string url, string hubName, string eventName)
        {
            Microsoft.AspNet.SignalR.Client.HubConnection hc = new Microsoft.AspNet.SignalR.Client.HubConnection(url);
            _hc = hc;
            IHubProxy proxy = hc.CreateHubProxy(hubName);
            proxy.On<string>(eventName, (data) =>
            {
                Console.WriteLine("SignalR RX: " + data);
                EventHubPoster.PostEventData(data);
            });
            Console.WriteLine("Connecting to SignalR hub...");
            hc.Error += OnSignalRError;
            hc.Closed += OnSignalRClosed;
            hc.StateChanged += OnSignalRStateChanged;
            hc.Reconnecting += OnSignalRReconnecting;
            hc.Reconnected += OnSignalRReconnected;
            hc.Start();
            
            Console.WriteLine("SignalR hub connected");
        }

        private static void OnSignalRReconnected()
        {
            Console.WriteLine("SignalR Reconnected");
        }

        private static void OnSignalRReconnecting()
        {
            Console.WriteLine("SignalR Reconnecting");
        }

        private static void OnSignalRStateChanged(StateChange obj)
        {
            Console.WriteLine("SignalR state changed: " + obj.NewState.ToString());
        }

        private static void OnSignalRClosed()
        {
            Console.WriteLine("SignalR connection closed");
            _hc.Start();
        }

        private static void OnSignalRError(Exception obj)
        {
            Console.WriteLine("SignalR Error: " + obj.Message);
        }
    }
}
