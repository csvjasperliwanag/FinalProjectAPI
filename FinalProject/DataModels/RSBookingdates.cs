using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProj.DataModels
{
    public class RSBookingdates
    {
        [JsonProperty("checkin")]
        public DateTimeOffset Checkin { get; set; }

        [JsonProperty("checkout")]
        public DateTimeOffset Checkout { get; set; }
    }
}