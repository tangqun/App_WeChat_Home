﻿using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL_9H
{
    public interface IUserInfoBLL
    {
        RESTfulModel Login(LoginReq model);

        UserInfoModel GetByToken(string token);
    }
}
