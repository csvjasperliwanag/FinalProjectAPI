using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharpProj.DataModels;

namespace RestSharpProj.Tests.TestData
{
    public class RSGenerateToken
    {
        public static RSLoginModel newToken()
        {
            return new RSLoginModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
