using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class MaterialInfoModel
    {
        [JsonProperty("mediaID")]
        public string MediaID { get; set; }
        [JsonProperty("content")]
        public List<NewsTypeMaterialInfoModel> NewsTypeMaterialInfoList { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("createTime")]
        public DateTime CreateTime { get; set; }
        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }
    }
}
