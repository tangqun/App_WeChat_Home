using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class Authorizer_Info
    {
        public string Nick_Name { get; set; }
        public string Head_Img { get; set; }
        public Service_Type_Info Service_Type_Info { get; set; }
        public Verify_Type_Info Verify_Type_Info { get; set; }
        public string User_Name { get; set; }
        public string Alias { get; set; }
        public string Qrcode_Url { get; set; }
        public Business_Info Business_Info { get; set; }
        public int IDC { get; set; }
        public string Principal_Name { get; set; }
    }
}
