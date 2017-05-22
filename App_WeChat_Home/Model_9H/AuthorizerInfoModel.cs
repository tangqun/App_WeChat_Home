using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class AuthorizerInfoModel
    {
        public int Id { get; set; }
        // 不直接让 id 和 authorizer_appid 直接对外
        public int User_Id { get; set; }

        public string Authorizer_AppId { get; set; }
        public string Nick_Name { get; set; }
        public string Head_Img { get; set; }
        public int Service_Type_Info { get; set; }
        public int Verify_Type_Info { get; set; }
        public string User_Name { get; set; }
        public string Alias { get; set; }

        // 二维码
        public string Qrcode_Url { get; set; }

        // business_info
        public int Open_Pay { get; set; }
        public int Open_Shake { get; set; }
        public int Open_Scan { get; set; }
        public int Open_Card { get; set; }
        public int Open_Store { get; set; }

        public int IDC { get; set; }
        public string Principal_Name { get; set; }

        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}
