using System;
using NUnit.Framework;
using RestSharp;
using RestSharpDemo.Model;
using RestSharpDemo.Utilities;

namespace RestSharpDemo
{
    public class ExamplePostMethod
    {
        [TestFixture]
        public class Tests
        {
            private RestClient _restClient;
            private RestRequest _restRequest;
            private const string BaseUrl = "https://reqres.in/";

            [SetUp]
            public void Setup()
            {
                _restClient = new RestClient(BaseUrl);
            }

            [Test]
            public void TestWithPostCall()
            {
                //Arrange
                _restRequest = new RestRequest("api/users", Method.POST);
                _restRequest.AddJsonBody(new {name = "morpheus"});
                _restRequest.AddJsonBody(new {job = "leader"});
                //Act
                var result = _restClient.Execute(_restRequest);
                var rResult = result.Deserializeresponse();
                var output = rResult["name"];
                //Assert
                Assert.That(output, Is.EqualTo("morpheus"),
                    "Test case Failed as the excepted values did not matched! ");
            }

            [Test]
            public void TestPostWithTypeClass()
            {
                _restRequest = new RestRequest("api/register", Method.POST);
                // _restRequest.RequestFormat == DataFormat.Xml;
                // _restRequest.AddJsonBody(new Users() {email= "eve.holt@resqres.in", password = "piston"});
                _restRequest.AddJsonBody(new Users() {email = "eve.holt@reqres.in", password = "piston"});
                var response = _restClient.Execute<Users>(_restRequest);
                Console.WriteLine("token value is :: " + response.Data.token);
                Assert.That(response.Data.token, Is.EqualTo("QpwL5tke4Pnpja7X4"));
                /* var rResult = response.Deserializeresponse();
                 var output = rResult["name"]; */
            }
        }
    }
}