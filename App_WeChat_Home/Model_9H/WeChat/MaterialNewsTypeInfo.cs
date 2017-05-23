using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class MaterialNewsTypeInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("thumb_media_id")]
        public string ThumbMediaID { get; set; }
        [JsonProperty("show_cover_pic")]
        public string ShowCoverPic { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("digest")]
        public string Digest { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("content_source_url")]
        public string ContentSourceUrl { get; set; }
    }
}
