using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTTPClient.DataModels
{
    public partial class HTTPBookModel
    {
        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("totalprice")]
        public long Totalprice { get; set; }

        [JsonProperty("depositpaid")]
        public bool Depositpaid { get; set; }

        [JsonProperty("bookingdates")]
        public HTTPBookingdates Bookingdates { get; set; }

        [JsonProperty("additionalneeds")]
        public string Additionalneeds { get; set; }
    }
    public partial class HTTPBookingdates
    {
        [JsonProperty("checkin")]
        public DateTimeOffset Checkin { get; set; }

        [JsonProperty("checkout")]
        public DateTimeOffset Checkout { get; set; }
    }

    public class HTTPBookingResModel
    {
        [JsonProperty("bookingid")]
        public long Bookingid { get; set; }

        [JsonProperty("booking")]
        public HTTPBookModel BookingDetails { get; set; }
    }

    public class HTTPLoginModel
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }

    public class HTTPTokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
