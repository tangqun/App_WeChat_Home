using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class MaterialListGetReq
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("offset")]
        public int Offset { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
