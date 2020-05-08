using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpDemo.Utilities;

namespace RestSharpDemo
{
    public class Assignment
    {
        [TestFixture]
        public class Tests
        {
            private RestClient _restClient;
            private RestRequest _restRequest;
            [SetUp]
            public void Setup()
            {
                _restClient = new RestClient("http://dummy.restapiexample.com");
            }
            [Test]
            public void PostCallCreateEmployee()
            {
                //Arrange
                _restRequest = new RestRequest("/api/v1/create", Method.POST);
                /* Note: not the ideal way to add json body
                _restRequest.AddJsonBody(new {name = "abc"});
                _restRequest.AddJsonBody(new {salary = "123"});
                _restRequest.AddJsonBody(new {age = "23"});
                */
                // ideal way to add json body
                _restRequest.AddJsonBody(new {name = "abc", salary = "123", age="23"});
                
                //Act
                var restResponse = _restClient.Execute(_restRequest);// here it will return the json object
                Console.WriteLine(" ****** Json object to string ******" + restResponse.Content);
                //parsing json object so that we can get key value pair
                JObject jobject = JObject.Parse(restResponse.Content);
                var jObjFinalOutput = jobject["data"];
                var actualName = jObjFinalOutput["name"];
                var salary = jObjFinalOutput["salary"];
                var age = jObjFinalOutput["age"];
                Console.WriteLine("********  The values are ******** " + "name:: " + actualName + "  "+ "age:: " + age + "  " +"salary:: " + salary);
                
                //Assert
                Assert.That(actualName.ToString, Is.EqualTo("abc"),"validation failed as the actual and excepted value did not match!");
            }
        }
    }
}