using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class UserInfoModel
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string BusinessID { get; set; }
        public string Mobile { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string RealName { get; set; }
        public int UserStat { get; set; }
        public int LoginErrorTimes { get; set; }
        public string Token { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
