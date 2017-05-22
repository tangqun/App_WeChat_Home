using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL_9H
{
    public interface IAuthBLL
    {
        string GetPreAuthCode();
        RESTfulModel RecvAuth(string auth_code, int expires_in, int user_id);
    }
}
