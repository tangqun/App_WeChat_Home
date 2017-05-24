﻿using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IAuthorizationInfoDAL
    {
        AuthorizationInfoModel GetModel(string authorizerAppID);
        // 授权
        int Insert(string authorizerAppID, string authorizerAccessTokenOld, string authorizerAccessToken, int expiresIn, string authorizerRefreshToken, DateTime authTime);
        // 二次授权
        bool Update(string authorizerAppID, string authorizerAccessTokenOld, string authorizerAccessToken, int expiresIn, string authorizerRefreshToken, DateTime authTime);
    }
}
