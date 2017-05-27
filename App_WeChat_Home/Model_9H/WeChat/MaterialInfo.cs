using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class MaterialInfo
    {
        [JsonProperty("media_id")]
        public string MediaID { get; set; }
        [JsonProperty("content")]
        public NewsTypeMaterial Content { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("update_time")]
        public DateTime UpdateTime { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
