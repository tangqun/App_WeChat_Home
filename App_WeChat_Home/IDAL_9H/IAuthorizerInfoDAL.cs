using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IAuthorizerInfoDAL
    {
        List<AuthorizerInfoModel> GetList(string userID);
        AuthorizerInfoModel GetModel(string authorizerAppID);
        int Insert(string userID, string authorizerAppID, string nickName, string headImg, int serviceTypeInfo, int verifyTypeInfo, string user_name, string alias, string qrcodeUrl, int openPay, int openShake, int openScan, int openCard, int openStore, int idc, string principalName, DateTime createTime);
        bool Update(string authorizerAppID, string nickName, string headImg, int serviceTypeInfo, int verifyTypeInfo, string alias, string qrcodeUrl, int openPay, int openShake, int openScan, int openCard, int openStore, int idc, string principalName, DateTime updateTime);
    }
}
