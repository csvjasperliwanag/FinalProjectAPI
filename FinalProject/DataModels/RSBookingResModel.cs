using RestSharpProj.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProj.DataModels
{
    public class RSBookingResModel
    {
        [JsonProperty("bookingid")]
        public long Bookingid { get; set; }

        [JsonProperty("booking")]
        public RSBookModel BookingDetails { get; set; }
    }
}
