using RestSharpProj.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharpProj.Helpers;

namespace RestSharpProj.Tests
{
    public class RSAPITest
    {
        public RestClient restClient { get; set; }
        public RSBookModel bookingModel { get; set; }

        public RSBookingHelper bookingHelper { get; set; }

        public string token;

        [TestInitialize]
        public async Task Initialize()
        {
            restClient = new RestClient();
            bookingHelper = new RSBookingHelper();
            token = await bookingHelper.ObtainToken(restClient);
        }
    }
}
