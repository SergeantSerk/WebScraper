using Steam.Interfaces;
using Steam.Models;
using Steam.WebApi;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Steam.Utilities
{
    public static class SteamFactory
    {
        //private static string _vpnUsername = Environment.GetEnvironmentVariable("VPNUsername");
        //private static string _vpnPassword = Environment.GetEnvironmentVariable("VPNPassword");
        

        public static HttpClient GetHttpClient()
        {
            return new HttpClient(handler:GetHttpClientHandler());
        }

        //public static WebProxy GetWebProxy(Uri address)
        //{
        //    return new WebProxy() { 
        //        Credentials = GetNetworkCredential(),
        //        BypassProxyOnLocal = false,
        //        UseDefaultCredentials = false,
        //        Address = address

        //    };
        //}

        //private static List<WebProxy> GetWebProxies ()
        //{
        //    var _ukProxy = new Uri("http://uk2173.nordvpn.com");
        //    var _ukProxy2 = new Uri("http://uk1914.nordvpn.com");

        //    var proxies = new List<WebProxy>()
        //    { GetWebProxy(_ukProxy),
        //        GetWebProxy(_ukProxy2)};

        //    return proxies;

        //}

        //private static List<HttpClientHandler> GetHttpClientHandlers()
        //{
        //    var handlers = new List<HttpClientHandler>();

        //    foreach(var proxy in GetWebProxies())
        //    {
        //        handlers.Add(new HttpClientHandler() { Proxy = proxy, UseProxy= true});
        //    }

        //    return handlers;
        //}

        //public static List<HttpClient> GetHttpClients()
        //{


        //    var clients = new List<HttpClient>();


        //    foreach(var handler in GetHttpClientHandlers())
        //    {
        //        clients.Add(new HttpClient(handler:handler));

        //    }

        //    return clients;
        //}

        //private static NetworkCredential GetNetworkCredential()
        //{

        //    return new NetworkCredential() {
        //        UserName = _vpnUsername,
        //        Password = _vpnPassword
        //    };
        //}

        private static HttpClientHandler GetHttpClientHandler()
        {
            return new HttpClientHandler();
        }

        public static ISteamAPI GetSteamAPI()
        {

            return new SteamAPI(GetHttpClient());
        }

     
    }
}
