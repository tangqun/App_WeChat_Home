using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class AuthorizerInfoModel
    {
        public int ID { get; set; }
        // 不直接让 id 和 authorizer_appid 直接对外
        public int UserID { get; set; }

        public string AuthorizerAppID { get; set; }
        public string NickName { get; set; }
        public string HeadImg { get; set; }
        public int ServiceTypeInfo { get; set; }
        public int VerifyTypeInfo { get; set; }
        public string UserName { get; set; }
        public string Alias { get; set; }

        // 二维码
        public string QrcodeUrl { get; set; }

        // business_info
        public int OpenPay { get; set; }
        public int OpenShake { get; set; }
        public int OpenScan { get; set; }
        public int OpenCard { get; set; }
        public int OpenStore { get; set; }

        public int IDC { get; set; }
        public string PrincipalName { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
