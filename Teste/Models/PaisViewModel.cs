using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.Models
{
    public class Pais
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("abbr")]
        public string abbr { get; set; }
        [JsonProperty("area")]
        public string area { get; set; }
        [JsonProperty("largest_city")]
        public string largest_city { get; set; }
        [JsonProperty("capital")]
        public string capital { get; set; }
    }

    public class RestResponse
    {
        [JsonProperty("messages")]
        public List<string> messages { get; set; }
        [JsonProperty("result")]
        public Pais result { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("RestResponse")]
        public RestResponse RestResponse { get; set; }
    }
}
