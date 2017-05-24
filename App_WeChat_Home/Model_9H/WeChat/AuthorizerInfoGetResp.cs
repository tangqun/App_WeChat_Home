using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class AuthorizerInfoGetResp
    {
        [JsonProperty("authorizer_info")]
        public AuthorizerInfo AuthorizerInfo { get; set; }
        [JsonProperty("authorization_info")]
        public AuthorizationInfo AuthorizationInfo { get; set; }
    }
}
