using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DesignPatterns.AzureRestApi
{
    internal class CloneBuildDefinition
    {

        const string PAT = "jygqacvrdpvcj7nb6tkqzm27jfp6vc3c3nc2wefpcki25ddnx3eq";
        const string ORGANISATIONNAME = "pnmsoftdevlabs";
        const string PROJECTNAME = "PNMsoft Sequence";
        const string AREA = "build";
        const string RESOURECE = "definitions";
        const string APIVERSION = "api-version=5.0";



        public void GetResult()
        {
            string filter = @"&path=\Patch";
            //encode your personal access token                   
            string credentials = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", PAT)));

            // ListOfProjectsResponse.Projects viewModel = null;

            //use the httpclient
            using (var client = new HttpClient())
            {
                // https://dev.azure.com/pnmsoftdevlabs/PNMsoft Sequence/_apis/build/definitions?api-version=4.1&path=\Patch
                // https://dev.azure.com/pnmsoftdevlabs/PNMsoft Sequence/_apis/builds/definitions?api-version=5.0&path=\Patch

                client.BaseAddress = new Uri($"https://dev.azure.com/{ORGANISATIONNAME}/{PROJECTNAME}/"); 
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                //connect to the REST endpoint            
                HttpResponseMessage response = client.GetAsync($"_apis/{AREA}/{RESOURECE}?{APIVERSION}{filter}&definitionId=143").Result;

                //check to see if we have a successful response
                if (response.IsSuccessStatusCode)
                {
                    //set the viewmodel from the content in the response
                  var  viewModel = response.Content.ReadAsStringAsync().Result;
                    dynamic data = JObject.Parse(viewModel);

                    // dynamic to json string 
                    //Newtonsoft.Json.JsonConvert.SerializeObject(data);

                    Console.Write(viewModel);
                    //var value = response.Content.ReadAsStringAsync().Result;
                }
            }
        }
    }
}


