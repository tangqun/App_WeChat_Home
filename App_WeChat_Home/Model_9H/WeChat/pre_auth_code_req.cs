using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class Pre_Auth_Code_Req
    {
        [JsonProperty("component_appid")]
        public string Component_AppId { get; set; }
    }
}
