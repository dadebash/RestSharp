using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharpDemo
{
    [TestFixture]
    public class Tests
    {
        private RestClient _restClient;
        private RestRequest _restRequest;
           [SetUp]
        public void Setup()
        {
             _restClient = new RestClient("https://reqres.in");
        }
       [Test]
        public void Test1()
        { 
            //Arrange
            _restRequest = new RestRequest("api/users?page=2",Method.GET);
            //Act
          var result = _restClient.Execute(_restRequest).Content;
            //Assert
            Console.WriteLine("This is the response:: " + result);
        }
        [Test]
        public void VerifyTotalNumOfRecords()
        {
           // RestClient restClient = new RestClient("https://reqres.in");
           // RestRequest restRequest = new RestRequest("api/users?page=2",Method.GET);
            var restResponse = _restClient.Execute(_restRequest);
            //inbuilt deserializer
            var jsonDesrializer = new JsonDeserializer();
            var output= jsonDesrializer.Deserialize<Dictionary<string,string>>(restResponse);
           var finaloutput = output["total"];
           Console.WriteLine("*** The deserialized is:: ***  "+ finaloutput);
            //JObject - NewTonSoft
            JObject jobject = JObject.Parse(restResponse.Content);
            var jObjFinalOutput = jobject["total"];
            Console.WriteLine("The deserialized value is::" + jObjFinalOutput);
            Assert.That(jObjFinalOutput.ToString(), Is.EqualTo("12"),"Total record does not match");
       }
        [TearDown]
        public void close()
        {
            Console.WriteLine("Execution done!!");
        }
    }
}