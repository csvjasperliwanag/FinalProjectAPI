using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference1;
using System.Linq;

namespace SOAP.Tests
{
    [TestClass]
    public class SOAPFinal
    {
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryList =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        private tCountryCodeAndName[] SoapCountryList()
        {
            var SOAPCountryList = countryList.ListOfCountryNamesByCode();
            return SOAPCountryList;
        }

        private static tCountryCodeAndName RandomCountryCode(tCountryCodeAndName[] countryList)
        {
            Random rd = new Random();
            int countryCount = countryList.Count() - 1;
            int randomNum = rd.Next(0, countryCount);
            var randomCountryCode = countryList[randomNum];

            return randomCountryCode;
        }

        [TestMethod]
        public void TestScenarioA()
        {
            var SOAPCountryList = SoapCountryList();
            var ListCountryCode = RandomCountryCode(SOAPCountryList);
            var CountryDetails = countryList.FullCountryInfo(ListCountryCode.sISOCode);

            Assert.AreEqual(ListCountryCode.sISOCode, ListCountryCode.sISOCode, "Country code doesn't match.");
            Assert.AreEqual(ListCountryCode.sName, ListCountryCode.sName, "Country name doesn't match.");
        }

        [TestMethod]
        public void TestScenarioB()
        {
            var SOAPCountryList = SoapCountryList();
            List<tCountryCodeAndName> SampleCountries = new List<tCountryCodeAndName>();

            for (int i = 0; i < 5; i++)
            {
                SampleCountries.Add(RandomCountryCode(SOAPCountryList));
            }

            foreach (var country in SampleCountries)
            {
                var countryISOCode = countryList.CountryISOCode(country.sName);
                Assert.AreEqual(country.sISOCode, countryISOCode, "Country code doesn't match.");
            }
        }
    }
}