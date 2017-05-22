using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
        public string Real_Name { get; set; }
        public int User_Stat { get; set; }
        public int Login_Error_Times { get; set; }
        public DateTime Create_Time { get; set; }
        public DateTime Update_Time { get; set; }
    }
}
