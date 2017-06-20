using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class LoginReq
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号不能为空")]
        public string Mobile { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
