using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model_9H
{
    public class BusinessInfo
    {
        [JsonProperty("open_pay")]
        public int OpenPay { get; set; }
        [JsonProperty("open_shake")]
        public int OpenShake { get; set; }
        [JsonProperty("open_scan")]
        public int OpenScan { get; set; }
        [JsonProperty("open_card")]
        public int OpenCard { get; set; }
        [JsonProperty("open_store")]
        public int OpenStore { get; set; }
    }
}
