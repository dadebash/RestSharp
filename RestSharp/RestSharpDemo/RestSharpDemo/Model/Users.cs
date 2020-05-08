using System.Collections.Generic;
using  Newtonsoft.Json;
namespace RestSharpDemo.Model

{
    public class Users
    {
        //Request payload data
        public string email {get; set; }
        public string password { get; set;}
        
        //Response data
        public string id { get; set; }
        public string token { get; set; }
    }
}