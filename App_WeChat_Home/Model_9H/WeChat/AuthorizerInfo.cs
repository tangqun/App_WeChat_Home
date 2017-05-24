using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class AuthorizerInfo
    {
        [JsonProperty("nick_name")]
        public string NickName { get; set; }
        [JsonProperty("head_img")]
        public string HeadImg { get; set; }
        [JsonProperty("service_type_info")]
        public ServiceTypeInfo ServiceTypeInfo { get; set; }
        [JsonProperty("verify_type_info")]
        public VerifyTypeInfo VerifyTypeInfo { get; set; }
        [JsonProperty("user_name")]
        public string UserName { get; set; }
        [JsonProperty("alias")]
        public string Alias { get; set; }
        [JsonProperty("qrcode_url")]
        public string QrcodeUrl { get; set; }
        [JsonProperty("business_info")]
        public BusinessInfo BusinessInfo { get; set; }
        [JsonProperty("idc")]
        public int IDC { get; set; }
        [JsonProperty("principal_name")]
        public string PrincipalName { get; set; }
    }
}
