using HTTPClient.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPClient.Tests.TestData
{
    public class HTTPGenerateBooking
    {
        public static HTTPBookModel newBooking()
        {
            DateTime dateTime = DateTime.Now;
            HTTPBookingdates bookingdates = new HTTPBookingdates();

            bookingdates.Checkin = dateTime;
            bookingdates.Checkout = dateTime.AddMonths(1).Date;
            return new HTTPBookModel
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
