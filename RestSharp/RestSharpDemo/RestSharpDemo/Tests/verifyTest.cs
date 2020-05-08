using System;
using System.Collections.Generic;
using System.Media;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharpDemo.Model;

namespace RestSharpDemo
{
    public class verifyTest
    {    
        [TestFixture]
        public class Test
        {
            private RestClient _restClient;
            private RestRequest _restRequest;
            private const string BaseUrl = "http://api.zippopotam.us";
            
            [SetUp]
            public void Setup()
            {
                _restClient = new RestClient(BaseUrl);
            }
            
            [Test, TestCaseSource("placesTestData")]
            public void Test_withTestSourceData(string countryCode, string pinCode, string placeName)
            {
                _restRequest = new RestRequest($"{countryCode}/{pinCode}", Method.GET);
                var response = _restClient.Execute(_restRequest);
                Console.WriteLine("*************** " + response.StatusCode);
                Console.WriteLine(response.Content);
             
                //Deserialize
                /*JObject output =JObject.Parse(response.Content);
                var op = output["country"];
                Console.WriteLine("+++++"  + op);*/
                var testVal = new JsonDeserializer().Deserialize<Location>(response);
                Console.WriteLine("testval is :: " + testVal.Places[0].PlaceName);
            }
            
            private static IEnumerable<TestCaseData> placesTestData()
            {
                yield return new TestCaseData("AU", "2140", "Homebush").SetName(
                    "check status code for 2140");
            }
        }
   }
}