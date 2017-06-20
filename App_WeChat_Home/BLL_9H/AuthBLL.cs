using DAL_9H;
using Helper_9H;
using IBLL_9H;
using IDAL_9H;
using Model_9H;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_9H
{
    public class AuthBLL: IAuthBLL
    {
        private IComponentAccessTokenDAL componentAccessTokenDAL = new ComponentAccessTokenDAL();
        private IAuthorizationInfoDAL authorizationInfoDAL = new AuthorizationInfoDAL();
        private IFuncInfoDAL funcInfoDAL = new FuncInfoDAL();
        private IAuthorizerInfoDAL authorizerInfoDAL = new AuthorizerInfoDAL();
        private ICodeMsgDAL codeMsgDAL = new CodeMsgDAL();

        public string GetPreAuthCode()
        {
            try
            {
                string componentAppId = ConfigHelper.ComponentAppId;

                string component_access_token = componentAccessTokenDAL.Get();
                string url = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token=" + component_access_token;

                LogHelper.Info("3、获取预授权码url: " + url);

                PreAuthCodeGetReq req = new PreAuthCodeGetReq();
                req.ComponentAppId = componentAppId;
                string requestBody = JsonConvert.SerializeObject(req);

                LogHelper.Info("3、获取预授权码pre_auth_code，requestBody: " + requestBody);

                string responseBody = HttpHelper.Post("", requestBody);

                LogHelper.Info("3、获取预授权码pre_auth_code，responseBody: " + responseBody);

                if (!string.IsNullOrEmpty(responseBody))
                {
                    PreAuthCodeGetResp resp = JsonConvert.DeserializeObject<PreAuthCodeGetResp>(responseBody);
                    if (resp != null)
                    {
                        return resp.PreAuthCode;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return null;
            }
        }

        public RESTfulModel RecvAuth(string authCode, int expiresIn, int userID)
        {
            try
            {
                string componentAppID = ConfigHelper.ComponentAppId;

                // 4、使用授权码换取公众号的接口调用凭据和授权信息
                AuthorizationInfoGetReq req = new AuthorizationInfoGetReq();
                req.ComponentAppId = componentAppID;
                req.AuthorizationCode = authCode;
                string requestBody = JsonConvert.SerializeObject(req);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息" + "\r\n\r\nrequestBody: " + requestBody);

                string componentAccessToken = componentAccessTokenDAL.Get();

                string responseBody = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token=" + componentAccessTokenDAL.Get(), requestBody);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息" + "\r\n\r\nrequestBody: " + requestBody + "\r\n\r\nresponseBody: " + responseBody);

                AuthorizationInfoGetResp resp = JsonConvert.DeserializeObject<AuthorizationInfoGetResp>(responseBody);
                #region 授权信息存数据库
                // 授权信息存数据库
                AuthorizationInfoModel authorizationInfoModel = authorizationInfoDAL.GetModel(resp.AuthorizationInfo.AuthorizerAppID);
                if (authorizationInfoModel != null)
                {
                    // 更新
                    bool res = authorizationInfoDAL.Update(
                        resp.AuthorizationInfo.AuthorizerAppID,
                        authorizationInfoModel.AuthorizerAccessToken,// 当前的置为旧的，用于消息延时
                        resp.AuthorizationInfo.AuthorizerAccessToken,
                        resp.AuthorizationInfo.ExpiresIn,
                        resp.AuthorizationInfo.AuthorizerRefreshToken,
                        DateTime.Now);

                    // 删除权限
                    funcInfoDAL.Delete(resp.AuthorizationInfo.AuthorizerAppID);
                    // 插入权限，不存在空集合
                    List<int> funcscopeCategoryIdList = resp.AuthorizationInfo.FuncInfo.Select(o => o.FuncscopeCategory.ID).ToList();
                    foreach (var funcscopeCategoryId in funcscopeCategoryIdList)
                    {
                        funcInfoDAL.Insert(resp.AuthorizationInfo.AuthorizerAppID, funcscopeCategoryId);
                    }

                    return Authorize(componentAppID, resp.AuthorizationInfo.AuthorizerAppID, componentAccessToken, userID);
                }
                else
                {
                    // 插入
                    int id = authorizationInfoDAL.Insert(
                        resp.AuthorizationInfo.AuthorizerAppID,
                        resp.AuthorizationInfo.AuthorizerAccessToken,
                        resp.AuthorizationInfo.AuthorizerAccessToken,
                        resp.AuthorizationInfo.ExpiresIn,
                        resp.AuthorizationInfo.AuthorizerRefreshToken,
                        DateTime.Now);

                    // 插入权限
                    List<int> funcscopeCategoryIdList = resp.AuthorizationInfo.FuncInfo.Select(o => o.FuncscopeCategory.ID).ToList();
                    foreach (var funcscopeCategoryId in funcscopeCategoryIdList)
                    {
                        funcInfoDAL.Insert(resp.AuthorizationInfo.AuthorizerAppID, funcscopeCategoryId);
                    }

                    // 授权成功
                    return Authorize(componentAppID, resp.AuthorizationInfo.AuthorizerAppID, componentAccessToken, userID);
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return new RESTfulModel() { Code = (int)CodeEnum.系统异常, Msg = codeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }

        private RESTfulModel Authorize(string componentAppID, string authorizerAppID, string componentAccessToken, int userID)
        {
            // 二次授权成功
            AuthorizerInfoGetReq req = new AuthorizerInfoGetReq();
            req.ComponentAppID = componentAppID;
            req.AuthorizerAppID = authorizerAppID;
            string requestBody = JsonConvert.SerializeObject(req);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息" + "\r\n\r\nrequestBody: " + requestBody);

            string responseBody = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?component_access_token=" + componentAccessToken, requestBody);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息" + "\r\n\r\nrequestBody: " + requestBody + "\r\n\r\nresponseBody: " + responseBody);

            AuthorizerInfoGetResp resp = JsonConvert.DeserializeObject<AuthorizerInfoGetResp>(responseBody);

            AuthorizerInfoModel authorizerInfoModel = authorizerInfoDAL.GetModel(authorizerAppID);
            if (authorizerInfoModel != null)
            {
                if (userID != authorizerInfoModel.UserID)
                {
                    return new RESTfulModel() { Code = (int)CodeEnum.失败, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.失败), "公众帐号已授权绑定，如有帐号争执，请联系客服") };
                }

                authorizerInfoDAL.Update(
                    authorizerAppID,
                    resp.AuthorizerInfo.NickName,
                    resp.AuthorizerInfo.HeadImg,
                    resp.AuthorizerInfo.ServiceTypeInfo.ID,
                    resp.AuthorizerInfo.VerifyTypeInfo.ID,
                    resp.AuthorizerInfo.Alias,
                    resp.AuthorizerInfo.QrcodeUrl,
                    resp.AuthorizerInfo.BusinessInfo.OpenPay,
                    resp.AuthorizerInfo.BusinessInfo.OpenShake,
                    resp.AuthorizerInfo.BusinessInfo.OpenScan,
                    resp.AuthorizerInfo.BusinessInfo.OpenCard,
                    resp.AuthorizerInfo.BusinessInfo.OpenStore,
                    resp.AuthorizerInfo.IDC,
                    resp.AuthorizerInfo.PrincipalName,
                    DateTime.Now);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.成功), "授权成功") };
            }
            else
            {
                authorizerInfoDAL.Insert(
                    userID,
                    authorizerAppID,
                    resp.AuthorizerInfo.NickName,
                    resp.AuthorizerInfo.HeadImg,
                    resp.AuthorizerInfo.ServiceTypeInfo.ID,
                    resp.AuthorizerInfo.VerifyTypeInfo.ID,
                    resp.AuthorizerInfo.UserName,
                    resp.AuthorizerInfo.Alias,
                    resp.AuthorizerInfo.QrcodeUrl,
                    resp.AuthorizerInfo.BusinessInfo.OpenPay,
                    resp.AuthorizerInfo.BusinessInfo.OpenShake,
                    resp.AuthorizerInfo.BusinessInfo.OpenScan,
                    resp.AuthorizerInfo.BusinessInfo.OpenCard,
                    resp.AuthorizerInfo.BusinessInfo.OpenStore,
                    resp.AuthorizerInfo.IDC,
                    resp.AuthorizerInfo.PrincipalName, 
                    DateTime.Now);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.成功), "授权成功") };
            }
        }
    }
}
