using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProj.Resources
{
    public class RSEndpoints
    {
        private const string BaseUrl = "https://restful-booker.herokuapp.com";
        public static string CreateBooking() => $"{BaseUrl}/booking";
        public static string GetBookingId(long bookingId) => $"{BaseUrl}/booking/{bookingId}";
        public static string UpdateBooking(long bookingId) => $"{BaseUrl}/booking/{bookingId}";
        public static string DeleteBooking(long bookingId) => $"{BaseUrl}/booking/{bookingId}";
        public static string Authenticate() => $"{BaseUrl}/auth";
    }
}
