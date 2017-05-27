using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class NewsTypeMaterialInfoModel
    {
        [JsonProperty("mediaID")]
        public string MediaID { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("thumbMediaID")]
        public string ThumbMediaID { get; set; }
        [JsonProperty("showCoverPic")]
        public int ShowCoverPic { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("digest")]
        public string Digest { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("contentSourceUrl")]
        public string ContentSourceUrl { get; set; }
    }
}
