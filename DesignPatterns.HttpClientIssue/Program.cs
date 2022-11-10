
using System.Net;
using System.Net.Http.Headers;
using DesignPatterns.HttpClientIssue;

var arguments = args;

#if DEBUG 
if (args.Length != 3)
{
    arguments = new string[] { "https://client1.insightbro.com/b/api/v1/clicked?campaignid=10&publisherid=15", "india", "desktop" };
}
#endif

if (arguments.Length!=3)
{
    Console.WriteLine("Argument list is not as per format.");
    Console.WriteLine("Please enter the arguments as per given format:");
    Console.WriteLine("arguments: <url> <country> <device>");
    return;
}

Dictionary<string, string> userAgents = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
userAgents.Add("Desktop", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.181 Safari/537.36");
userAgents.Add("IOS", "Mozilla/5.0 (iPhone; CPU iPhone OS 13_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.5 Mobile/15E148 Safari/604.1");
userAgents.Add("Android", "Mozilla/5.0 (Linux; Android 10; Pixel) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Mobile Safari/537.36");

var InitialUrl = arguments[0];
string country = arguments[1];
string device = arguments[2];

string? countryCode = ISO.Countries.FirstOrDefault(c => c.Name == country)?.TwoLetterCode;
string? deviceUserAgent;
userAgents.TryGetValue(device,out deviceUserAgent);

HttpClientHandler handler = new HttpClientHandler();
handler.AllowAutoRedirect = false;

// Create an HttpClient object
HttpClient httpClient = new HttpClient(handler);



while (true)
{
    Console.Write(InitialUrl);

    var request = new HttpRequestMessage()
    {
        RequestUri = new Uri(InitialUrl),
        Method = HttpMethod.Get        
    };

    if (!string.IsNullOrEmpty(countryCode))
    {
        //request.Headers.AcceptLanguage.TryParseAdd("en-gb;q=0.8");
        request.Headers.AcceptLanguage.TryParseAdd($"en-{countryCode}");
    }

    if (!string.IsNullOrEmpty(device))
    {        
        request.Headers.UserAgent.TryParseAdd(deviceUserAgent);
    }

    HttpResponseMessage clickResponse = await httpClient.SendAsync(request);

    //var clickResponse = await httpClient.GetAsync(InitialUrl);

    int responseStatus = (int)clickResponse.StatusCode;

    if (responseStatus >= 300 && responseStatus <= 308)
    {
        // continue on redirect
        Console.WriteLine($" Got Status Code : {responseStatus}");
        InitialUrl = clickResponse.Headers.Location?.AbsoluteUri;
    }
    else if (responseStatus >= 400 && responseStatus <= 511)
    {
        //break because of error
        Console.WriteLine($" Got Status Code : {responseStatus}");
        break;
    }
    else if (clickResponse.IsSuccessStatusCode)
    {
        // break on success
        Console.WriteLine($" Got Status Code : {responseStatus}");
        break;
    }
}

Console.WriteLine($"Finished tracking {arguments[0]}");


