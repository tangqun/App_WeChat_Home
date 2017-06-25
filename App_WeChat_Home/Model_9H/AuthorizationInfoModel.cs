using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_9H
{
    public class AuthorizationInfoModel
    {
        public string AuthorizerAccessTokenOld { get; set; }
        public string AuthorizerAccessToken { get; set; }
        public string JSAPITicket { get; set; }
        public string APITicket { get; set; }
    }
}
