using RestSharpProj.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpProj.Resources;
using RestSharp;
using RestSharpProj.Tests.TestData;
using RestSharpProj.DataModels;

namespace RestSharpProj.Helpers
{
    public class RSBookingHelper
    {
        public async Task<RestResponse> CreateBooking(RestClient client)
        {
            var bookingData = RSGenerateBooking.demoBooking();
            var postRequest = new RestRequest(RSEndpoints.CreateBooking())
                .AddJsonBody(bookingData)
                .AddHeader("Accept", "application/json");

            var RSresponses = await client.ExecutePostAsync(postRequest);

            return RSresponses;
        }
        public async Task<RSBookModel> GetBookingId(RestClient client, long id) 
        {
            var request = new RestRequest(RSEndpoints.GetBookingId(id))
                .AddHeader("Accept", "application/json");
            var response = await client.ExecuteGetAsync<RSBookModel>(request);

            return response.Data;
        }
        public async Task<RestResponse> UpdateBooking(RestClient client, long id, RSBookModel updatedBooking, string token)
        {
            var request = new RestRequest(RSEndpoints.GetBookingId(id))
                .AddJsonBody(updatedBooking)
                .AddHeader("Accept", "application/json")
                .AddHeader("Cookie", $"token={token}");
            var response = await client.ExecutePutAsync<RSBookModel>(request);

            return response;
        }

        public async Task<RestResponse> DeleteBooking(RestClient client, long id, string token)
        {
            var postRequest = new RestRequest(RSEndpoints.DeleteBooking(id))
                .AddHeader("Accept", "application/json")
                .AddHeader("Cookie", $"token={token}");

            var response = await client.DeleteAsync(postRequest);

            return response;
        }

        public async Task<string> ObtainToken(RestClient client)
        {
            var loginData = RSGenerateToken.newToken();

            var request = new RestRequest(RSEndpoints.Authenticate())
                .AddJsonBody(loginData)
                .AddHeader("Accept", "application/json");

            var response = await client.ExecutePostAsync(request);
            var content = JsonConvert.DeserializeObject<RSTokenModel>(response.Content);

            return content.Token;
        }
    }
}
