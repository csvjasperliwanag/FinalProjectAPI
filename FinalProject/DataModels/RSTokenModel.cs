using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProj.DataModels
{
    public class RSTokenModel
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
