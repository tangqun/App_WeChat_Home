using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class MaterialListGetResp
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("item_count")]
        public int ItemCount { get; set; }
        [JsonProperty("item")]
        public List<MaterialInfo> Item { get; set; }
    }
}
