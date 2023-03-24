using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HTTPClient.DataModels;
using System.Collections.Generic;
using HTTPClient.Helpers;
using HTTPClient.Resources;
using HTTPClient.Tests.TestData;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HTTPClient.Tests
{
    [TestClass]
    public class HTTPBaseTest
    {
        public HttpClient http_Client { get; set; }
        public HTTPBookModel bookingModel { get; set; }
        public HTTPBookingHelper bookingHelper { get; set; }
        public string token;

        [TestInitialize]
        public async Task Initialize()
        {
            http_Client = new HttpClient();
            bookingHelper = new HTTPBookingHelper();
            token = await bookingHelper.GetToken(http_Client);
        }
    }
    [TestClass]
    public class TestScenarioA : HTTPBaseTest
    {
        private readonly List<HTTPBookingResModel> cleanUpList = new List<HTTPBookingResModel>();
        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Delete, HTTPEndpoints.DeleteBooking(data.Bookingid), null, token);
            }
        }
        [TestMethod]
        public async Task HTTPCreateBooking()
        {
            //Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Post, HTTPEndpoints.CreateBooking(), HTTPGenerateBooking.newBooking());
            var postData = JsonConvert.DeserializeObject<HTTPBookingResModel>(httpResponse.Content.ReadAsStringAsync().Result);
            cleanUpList.Add(postData);

            //Act
            var getResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Get, HTTPEndpoints.GetBookingId(postData.Bookingid));
            var getData = JsonConvert.DeserializeObject<HTTPBookModel>(getResponse.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(httpResponse.StatusCode, HttpStatusCode.OK, "Failed due to wrong status code.");
            Assert.AreEqual(postData.BookingDetails.Firstname, getData.Firstname, "Firstname does not match.");
            Assert.AreEqual(postData.BookingDetails.Lastname, getData.Lastname, "Lastname does not match.");
            Assert.AreEqual(postData.BookingDetails.Totalprice, getData.Totalprice, "Total Price does not match.");
            Assert.AreEqual(postData.BookingDetails.Depositpaid, getData.Depositpaid, "Deposit Paid does not match.");
            Assert.AreEqual(postData.BookingDetails.Bookingdates.Checkout, getData.Bookingdates.Checkout, "Checkout Date does not match.");
            Assert.AreEqual(postData.BookingDetails.Bookingdates.Checkin, getData.Bookingdates.Checkin, "Checkin Date does not match."); ;
        }
    }
    [TestClass]
    public class TestScenarioB : HTTPBaseTest
    {
        private readonly List<HTTPBookingResModel> cleanUpList = new List<HTTPBookingResModel>();
        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Delete, HTTPEndpoints.DeleteBooking(data.Bookingid), null, token);
            }
        }
        [TestMethod]
        public async Task HTTPUpdateBooking()
        {
            //Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Post, HTTPEndpoints.CreateBooking(), HTTPGenerateBooking.newBooking());
            var postData = JsonConvert.DeserializeObject<HTTPBookingResModel>(httpResponse.Content.ReadAsStringAsync().Result);
            cleanUpList.Add(postData);

            DateTime dateTime = DateTime.Now;
            HTTPBookingdates bookingdates = new HTTPBookingdates();

            bookingdates.Checkin = dateTime;
            bookingdates.Checkout = dateTime.AddMonths(1).Date;

            var updateBooking = new HTTPBookModel()
            {
                Firstname = "Jasper",
                Lastname = "Liwanag",
                Totalprice = 100,
                Depositpaid = true,
                Bookingdates = bookingdates,
                Additionalneeds = "Placeholder Name"
            };

            //Arrange
            var updateResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Put, HTTPEndpoints.UpdateBooking(postData.Bookingid), updateBooking, token);
            var updateData = JsonConvert.DeserializeObject<HTTPBookModel>(updateResponse.Content.ReadAsStringAsync().Result);

            //Act
            var getResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Get, HTTPEndpoints.GetBookingId(postData.Bookingid));
            var getData = JsonConvert.DeserializeObject<HTTPBookModel>(getResponse.Content.ReadAsStringAsync().Result);

            //Assert
            Assert.AreEqual(updateResponse.StatusCode, HttpStatusCode.OK, "Failed due to wrong status code.");
            Assert.AreEqual(updateData.Firstname, getData.Firstname, "Firstname does not match.");
            Assert.AreEqual(updateData.Lastname, getData.Lastname, "Lastname does not match.");
            Assert.AreEqual(updateData.Totalprice, getData.Totalprice, "Total Price does not match.");
            Assert.AreEqual(updateData.Depositpaid, getData.Depositpaid, "Deposit Paid does not match.");
            Assert.AreEqual(updateData.Bookingdates.Checkout, getData.Bookingdates.Checkout, "Checkout Date does not match.");
            Assert.AreEqual(updateData.Bookingdates.Checkin, getData.Bookingdates.Checkin, "Checkin Date does not match."); ;
        }
    }

    [TestClass]
    public class TestScenarioC : HTTPBaseTest
    {
        private readonly List<HTTPBookingResModel> cleanUpList = new List<HTTPBookingResModel>();
        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Delete, HTTPEndpoints.DeleteBooking(data.Bookingid), null, token);
            }
        }
        [TestMethod]
        public async Task HTTPDeleteBooking()
        {
            //Arrange
            var httpResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Post, HTTPEndpoints.CreateBooking(), HTTPGenerateBooking.newBooking());
            var postData = JsonConvert.DeserializeObject<HTTPBookingResModel>(httpResponse.Content.ReadAsStringAsync().Result);

            //Act
            var deleteResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Delete, HTTPEndpoints.DeleteBooking(postData.Bookingid), null, token);

            //Assert
            Assert.AreEqual(deleteResponse.StatusCode, HttpStatusCode.Created, "Failed due to wrong status code.");
        }
    }

    [TestClass]
    public class TestScenarioD : HTTPBaseTest
    {
        private readonly List<HTTPBookingResModel> cleanUpList = new List<HTTPBookingResModel>();
        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var deleteResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Delete, HTTPEndpoints.DeleteBooking(data.Bookingid), null, token);
            }
        }
        [TestMethod]
        public async Task Validate_GetBooking()
        {
            var getResponse = await bookingHelper.SendAsyncFunction(http_Client, HttpMethod.Get, HTTPEndpoints.GetBookingId(-25));
            Assert.AreEqual(getResponse.StatusCode, HttpStatusCode.NotFound, "Failed due to specified ID not existing.");
        }
    }
}