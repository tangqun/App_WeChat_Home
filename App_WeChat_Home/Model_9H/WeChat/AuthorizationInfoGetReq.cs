using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class AuthorizationInfoGetReq
    {
        [JsonProperty("component_appid")]
        public string ComponentAppId { get; set; }
        [JsonProperty("authorization_code")]
        public string AuthorizationCode { get; set; }
    }
}
