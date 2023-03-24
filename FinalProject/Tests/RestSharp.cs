using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharpProj.Helpers;
using RestSharpProj.Resources;
using RestSharpProj.DataModels;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using RestSharpProj.Tests;
using Newtonsoft.Json;

namespace RestSharpProj.Tests
{
    [TestClass]
    public class RestSharp : RSAPITest
    {
        private readonly List<RSBookingResModel> bookingCleanUpList = new List<RSBookingResModel>();

        [TestCleanup]
        public async Task CleanUp()
        {
            foreach (var data in bookingCleanUpList)
            {
                var deleteResponse = await bookingHelper.DeleteBooking(restClient, data.Bookingid, token);
            }
        }
        [TestMethod]
        public async Task TestScenarioA()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var postData = JsonConvert.DeserializeObject<RSBookingResModel>(response.Content);

            bookingCleanUpList.Add(postData);

            //Act
            var getData = await bookingHelper.GetBookingId(restClient, postData.Bookingid);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Failed due to wrong status code.");
            Assert.AreEqual(postData.BookingDetails.Firstname, getData.Firstname, "Firstname does not match.");
            Assert.AreEqual(postData.BookingDetails.Lastname, getData.Lastname, "Lastname does not match.");
            Assert.AreEqual(postData.BookingDetails.Totalprice, getData.Totalprice, "Total Price does not match.");
            Assert.AreEqual(postData.BookingDetails.Depositpaid, getData.Depositpaid, "Deposit Paid does not match.");
            Assert.AreEqual(postData.BookingDetails.Bookingdates.Checkout, getData.Bookingdates.Checkout, "Checkout Date does not match.");
            Assert.AreEqual(postData.BookingDetails.Bookingdates.Checkin, getData.Bookingdates.Checkin, "Checkin Date does not match."); ;
        }

        [TestMethod]
        public async Task TestScenarioB()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<RSBookingResModel>(response.Content);
            bookingCleanUpList.Add(newBooking);

            DateTime dateTime = DateTime.Now;
            RSBookingdates bookingdates = new RSBookingdates();

            bookingdates.Checkin = dateTime;
            bookingdates.Checkout = dateTime.AddMonths(1).Date;

            var updateBooking = new RSBookModel()
            {
                Firstname = "Jasper",
                Lastname = "Liwanag",
                Totalprice = 100,
                Depositpaid = true,
                Bookingdates = bookingdates,
                Additionalneeds = "Placeholder Name"
            };

            //Act
            var updateRes = await bookingHelper.UpdateBooking(restClient, newBooking.Bookingid, updateBooking, token);
            var updateData = JsonConvert.DeserializeObject<RSBookModel>(updateRes.Content);

            var BookingData = await bookingHelper.GetBookingId(restClient, newBooking.Bookingid);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK, "Failed due to wrong status code.");
            Assert.AreEqual(updateData.Firstname, BookingData.Firstname, "Firstname does not match.");
            Assert.AreEqual(updateData.Lastname, BookingData.Lastname, "Lastname does not match.");
            Assert.AreEqual(updateData.Totalprice, BookingData.Totalprice, "Total Price does not match.");
            Assert.AreEqual(updateData.Depositpaid, BookingData.Depositpaid, "Deposit Paid does not match.");
            Assert.AreEqual(updateData.Bookingdates.Checkout, BookingData.Bookingdates.Checkout, "Checkout Date does not match.");
            Assert.AreEqual(updateData.Bookingdates.Checkin, BookingData.Bookingdates.Checkin, "Checkin Date does not match."); ;
        }
        [TestMethod]
        public async Task TestScenarioC()
        {
            //Arrange
            var response = await bookingHelper.CreateBooking(restClient);
            var newBooking = JsonConvert.DeserializeObject<RSBookingResModel>(response.Content);

            //Act
            var deleteData = await bookingHelper.DeleteBooking(restClient, newBooking.Bookingid, token);

            //Assert
            Assert.AreEqual(deleteData.StatusCode, HttpStatusCode.Created, "Failed due to wrong status code.");
        }
        
        [TestMethod]
        public async Task TestScenarioD()
        {
            //Arrange
            var request = new RestRequest(RSEndpoints.GetBookingId(-25))
               .AddHeader("Accept", "application/json");

            //Act
            var response = await restClient.ExecuteGetAsync<RSBookModel>(request);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound, "Failed due to specified ID not existing.");
        }
    }
}