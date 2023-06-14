using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the REST API endpoint URL
            string apiUrl = "https://feeds.dev.azure.com/pnmsoftdevlabs/_apis/packaging/Feeds/development/packages?api-version=7.0&packageNameQuery=Sequence&includeAllVersions=true";

            // Define the authentication header values
            string authHeaderKey = "Authorization";
            string authHeaderValue = "Basic OjZ0Y2l2cGU1eTZtenZ0NzN5b3V3eGxhN3hkN3dwZ3VtdDJwYmRkcGxsejdzZWhzdXFxc3E=";

            Dictionary<string, List<string>> toBeDeleated = new Dictionary<string, List<string>>();

            string responseContent = string.Empty;
            // Create an instance of the HttpClient class
            using (var httpClient = new HttpClient())
            {
                // Add the authentication header to the HTTP client
                httpClient.DefaultRequestHeaders.Add(authHeaderKey, authHeaderValue);

                // Make a GET request to the REST API
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).Result;

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Get the response content as a string
                    responseContent = response.Content.ReadAsStringAsync().Result;

                    // Print the response content to the console
                    //Console.WriteLine(responseContent);
                }
                else
                {
                    // Print the error message to the console
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }

            }

            Version lowerVersion = Version.Parse("10.2.1");
            Version upperVersion = Version.Parse("10.3");

            if (!string.IsNullOrEmpty(responseContent))
            {
                JObject jObject = JObject.Parse(responseContent);
                var packages = jObject["value"].Children().ToList();
                foreach (var package in packages)
                {
                    //Console.WriteLine(package["name"]);
                    var versions = package["versions"].Children().ToList();
                    List<string> versionsList = new List<string>();
                    foreach (var ver in versions)
                    {
                        var v = Version.Parse(ver["version"].ToString());
                        if (v >= lowerVersion && v < upperVersion)
                        {
                            versionsList.Add(v.ToString());
                            //Console.WriteLine(v);
                        }
                    }
                    toBeDeleated.Add(package["name"].ToString(), versionsList);

                    //System.Threading.Thread.Sleep(500);
                }
            }

            string jsonString = JsonConvert.SerializeObject(toBeDeleated);



            string deleteNugetsPackageVersionApiUrl = "https://pkgs.dev.azure.com/pnmsoftdevlabs/_apis/packaging/feeds/development/nuget/packages/{0}/versions/{1}?api-version=7.0";

            // Create an instance of the HttpClient class
            using (var httpClient = new HttpClient())
            {
                // Add the authentication header to the HTTP client
                httpClient.DefaultRequestHeaders.Add(authHeaderKey, authHeaderValue);
                bool exitLoop = false;
                foreach (var package in toBeDeleated)
                {
                    foreach (var v in package.Value)
                    {
                        string url = string.Format(deleteNugetsPackageVersionApiUrl, package.Key, v);
                        Console.WriteLine($"Deleting version : {v} for Package : {package.Key}. \n Delete API URL:  {url}");

                        HttpResponseMessage response = httpClient.DeleteAsync(url).Result;
                        // Check if the request was successful
                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Package Deleted");
                        }
                        else
                        {
                            // Print the error message to the console
                            Console.WriteLine("Request failed with status code: " + response.StatusCode);
                            exitLoop = true;
                            break;
                        }
                        System.Threading.Thread.Sleep(300);
                    }
                    if (exitLoop)
                    {
                        break;
                    }
                }
                // Make a GET request to the REST API
            }

            // Wait for the user to press a key before exiting the app
            Console.ReadKey();
        }
    }
}
