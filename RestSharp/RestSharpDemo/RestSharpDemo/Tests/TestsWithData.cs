using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;

namespace RestSharpDemo
{
    public class TestsWithData
    {
       // public class ExamplePostMethod
       // {
            [TestFixture]
            public class Tests
            {
                private RestClient _restClient;
                private RestRequest _restRequest;
                private const string BaseUrl = "http://api.zippopotam.us";

                [SetUp]
                public void Setup()
                {
                    _restClient = new RestClient(BaseUrl);
                }

                [TestCase("IN", "110001", HttpStatusCode.OK,
                    TestName = "check for status code in India with pin code 110001")]
                [TestCase("AU", "2140", HttpStatusCode.OK,
                    TestName = "check for status code in Aus with pin code 2140")]
                [TestCase("IN", "323423", HttpStatusCode.NotFound,
                    TestName = "check for status code in India with pin code 323423")]
                public void TestWithPostCal_withData(string countryCode, string pinCode,
                    HttpStatusCode ExceptedHttpsStatusCode)
                {
                    //Arrange
                    //  _restRequest = new RestRequest("/us/90210", Method.GET);
                    _restRequest = new RestRequest($"{countryCode}/{pinCode}", Method.GET);
                    //Act
                    var result = _restClient.Execute(_restRequest);
                    //Assert
                    Console.WriteLine("This is the response:: " + result.ToString());
                    Assert.That(result.StatusCode, Is.EqualTo(ExceptedHttpsStatusCode));
                }

                [Test, TestCaseSource("placesTestData")]
                public void TestWithPostCal_withTestcaseSourceData(string countryCode, string pinCode, string placeName)
                {
                    //Arrange
                    //_restRequest = new RestRequest("/us/90210", Method.GET);
                    _restRequest = new RestRequest($"{countryCode}/{pinCode}", Method.GET);
                    //Act
                    var response = _restClient.Execute(_restRequest);
                    //var output = response.Deserializeresponse();
                    
                    var output = new JsonDeserializer().Deserialize<Location>(response);
                   // Console.WriteLine(output.Country);
                    Console.WriteLine("******" + output.Places[0].PlaceName);
                    //Assert
                     Assert.That(output.Places[0].PlaceName, Is.EqualTo(placeName));
                }
                private static IEnumerable<TestCaseData> placesTestData()
                {
                    yield return new TestCaseData("AU", "2140", "Homebush").SetName(
                        "check status code for 2140");
                }
            }
    }
}