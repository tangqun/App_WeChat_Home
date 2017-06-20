using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IUserInfoDAL
    {
        UserInfoModel GetModel(string mobile);

        bool UpdateToken(string bID, string token, DateTime dt);

        UserInfoModel GetByToken(string token);
    }
}
