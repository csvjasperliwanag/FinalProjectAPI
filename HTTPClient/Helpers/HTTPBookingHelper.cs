using HTTPClient.DataModels;
using HTTPClient.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient.Helpers
{
    public class HTTPBookingHelper
    {
        public async Task<HttpResponseMessage> SendAsyncFunction(HttpClient client, HttpMethod method, string url, HTTPBookModel data = null, string auth = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.Method = method;
            httpRequestMessage.RequestUri = new Uri(url);
            httpRequestMessage.Headers.Add("Accept", "application/json");
            httpRequestMessage.Headers.Add("Cookie", $"token={auth}");

            if (data != null)
            {
                var request = JsonConvert.SerializeObject(data);
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }
            var httpResponse = await client.SendAsync(httpRequestMessage);
            return httpResponse;
        }

        public async Task<string> GetToken(HttpClient client)
        {

            var loginData = new HTTPLoginModel()
            {
                Username = "admin",
                Password = "password123"
            };

            var payload = JsonConvert.SerializeObject(loginData);
            var postRequest = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(HTTPEndpoints.Authenticate(), postRequest);

            var tokenResponse = JsonConvert.DeserializeObject<HTTPTokenModel>(response.Content.ReadAsStringAsync().Result);

            return tokenResponse.Token;
        }
    }
}
