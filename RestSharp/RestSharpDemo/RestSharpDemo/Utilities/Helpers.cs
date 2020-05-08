using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;

namespace RestSharpDemo.Utilities
{
    public static class Helper
    {
        public static Dictionary<string, string> Deserializeresponse(this IRestResponse restResponse)
        {
            var jsonObj = new JsonDeserializer().Deserialize<Dictionary<string, string>>(restResponse);
            return jsonObj;
        }
        public static string DeserializeresponseUsingJObject(this IRestResponse restResponse, string responseobj)
        {
            var jObject = JObject.Parse(restResponse.Content);
            return jObject[responseobj]?.ToString();

        }
    }
}