using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;

namespace TMDApp
{
    public class WatcherFactory
    {
        public static IWatchers createInstance()
        {
            string ipAddr = null;
            int port = 0;

            bool simMode = true;

            //running the actual quividi, values take from appconfig
            //ipAddr= ConfigurationManager.AppSettings["QUIVIDI_HOST"];
            //port = int.Parse(ConfigurationManager.AppSettings["QUIVIDI_PORT"]);

            return new QuividiProcessor(ipAddr, port, simMode);
        }
    }
}
