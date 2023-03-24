using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpProj.DataModels;

namespace RestSharpProj.Tests.TestData
{
    public class RSGenerateBooking
    {
        public static RSBookModel demoBooking()
        {
            DateTime dateTime = DateTime.Now;
            RSBookingdates bookingdates = new RSBookingdates();

            bookingdates.Checkin = dateTime;
            bookingdates.Checkout = dateTime.AddMonths(1).Date;

            return new RSBookModel
            {
                Firstname = "Richard",
                Lastname = "Solas",
                Totalprice = 100,
                Depositpaid = true,
                Bookingdates = bookingdates,
                Additionalneeds = "Placeholder Name"
            };
        }
    }
}
