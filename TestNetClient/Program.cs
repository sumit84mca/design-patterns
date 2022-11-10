using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestNetClient
{
    internal class Program
    {
        static void Main(string[] args)
        {

            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;

            // Create an HttpClient object
            HttpClient httpClient = new HttpClient(handler);
            var InitialUrl = "https://acebo.gotrackier.com/click?campaign_id=1224&pub_id=4";

            while (true)
            {
                Console.WriteLine(InitialUrl);

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(InitialUrl),
                    Method = HttpMethod.Get,
                };

                // HttpResponseMessage clickResponse = httpClient.SendAsync(request).Result;

                var clickResponse = httpClient.GetAsync(InitialUrl).Result;

                int responseStatus = (int)clickResponse.StatusCode;

                if (responseStatus >= 300 && responseStatus <= 308)
                {
                    // continue on redirect        
                    InitialUrl = clickResponse.Headers.Location.AbsoluteUri;
                }
                else if (responseStatus >= 400 && responseStatus <= 511)
                {
                    //break because of error
                    break;
                }
                else if (clickResponse.IsSuccessStatusCode)
                {
                    // break on success
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
