﻿using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IFuncInfoDAL
    {
        List<FuncInfoModel> GetList(string authorizerAppID);
        int Insert(string authorizerAppID, int funcscopeCategoryID);
        bool Delete(string authorizerAppID);
    }
}
