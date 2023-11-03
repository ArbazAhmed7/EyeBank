using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TransportManagement.Models
{
    public class ApiResponse
    {
        public async Task<string> Getto(string BaseUrl, string routeURL)
        {
            //using (var client = new HttpClient())
            //{
            //    HttpResponseMessage Res = await client.GetAsync(BaseUrl+routeURL);
            //    ////Passing service base url
            //    //client.BaseAddress = new Uri(BaseUrl);
            //    //client.DefaultRequestHeaders.Clear();
            //    ////Define request data format
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    //Sending request to find web api REST service resource GetAllEmployees using HttpClient
            //    //Checking the response is successful or not which is sent using HttpClient
            //    var Response = "";
            //    if (Res.IsSuccessStatusCode)
            //    {
            //        //Storing the response details recieved from web api
            //        //Response = Res.Content.ReadAsStringAsync().Result;
            //        Response =await Res.Content.ReadAsStringAsync();
            //    }
            //    return Response;
            //}

            //string apiUrl = "https://your-api-url/api/sample"; // Replace with your actual API URL

            using (HttpClient client = new HttpClient())
            {
                string Response = "";
                try
                { 
                    // Call the API and get the response
                    HttpResponseMessage response = await client.GetAsync(BaseUrl+routeURL);
                    response.EnsureSuccessStatusCode();

                    // Read the response content
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // Process the response (parse, display, etc.)
                    // For example, if the API returns JSON:
                    // var responseData = JsonConvert.DeserializeObject<ResponseModel>(responseBody);
                    // Label1.Text = responseData.Message;
                }
                catch (HttpRequestException ex)
                {
                    // Handle any exceptions that occurred during the API call
                    Response = $"Error: {ex.Message}";
                }
                return Response;
            }
        }

        public async Task<string> Get(string BaseUrl, string routeURL)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage Res = await client.GetAsync(BaseUrl + routeURL);
                ////Passing service base url
                //client.BaseAddress = new Uri(BaseUrl);
                //client.DefaultRequestHeaders.Clear();
                ////Define request data format
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                //HttpResponseMessage Res = await client.GetAsync(routeURL);
                //Checking the response is successful or not which is sent using HttpClient
                var Response = "";
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    //Response = Res.Content.ReadAsStringAsync().Result;
                    Response = await Res.Content.ReadAsStringAsync();
                }
                return Response;
            }
        }


        public async Task<string> PostAsyn(string BaseUrl, string routeURL, string modal)
        {
            var Response = "";
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonRes = JsonConvert.SerializeObject(modal);
                var requestContent = new StringContent(jsonRes, Encoding.UTF8, "application/json");

                //Sending request to find web api REST service resource 
                HttpResponseMessage Res = await client.PostAsync(routeURL, requestContent);
                //Thread.Sleep(2000);

                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    Response = Res.Content.ReadAsStringAsync().Result;
                }
                return Response;
            }
        }
    }
}