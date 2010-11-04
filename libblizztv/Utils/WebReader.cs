using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace LibBlizzTV.Utils
{
    public static class WebReader
    {
        public static string Read(string url)
        {
            string buffer;
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                using (StreamReader reader = new StreamReader(client.OpenRead(url)))
                {
                    buffer = reader.ReadToEnd();                    
                }
            }
            return buffer;
        }
    }
}
