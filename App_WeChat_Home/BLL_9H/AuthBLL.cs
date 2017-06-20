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

        public string GetPreAuthCodeUrl()
        {
            try
            {
                string componentAppId = ConfigHelper.ComponentAppId;

                string componentAccessToken = componentAccessTokenDAL.Get();
                string url = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token=" + componentAccessToken;

                LogHelper.Info("3、获取预授权码pre_auth_code url", url);

                PreAuthCodeGetReq req = new PreAuthCodeGetReq();
                req.ComponentAppId = componentAppId;
                string requestBody = JsonConvert.SerializeObject(req);

                LogHelper.Info("3、获取预授权码pre_auth_code requestBody", requestBody);

                string responseBody = HttpHelper.Post("", requestBody);

                LogHelper.Info("3、获取预授权码pre_auth_code responseBody", responseBody);

                PreAuthCodeGetResp resp = JsonConvert.DeserializeObject<PreAuthCodeGetResp>(responseBody);

                string redirectUri = ConfigHelper.Domain + "home/recvauth";

                return "https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid=" + componentAppId + "&pre_auth_code=" + resp.PreAuthCode + "&redirect_uri=" + redirectUri;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                // want ex
                return null;
            }
        }

        public RESTfulModel RecvAuth(string authCode, int expiresIn, string userID)
        {
            try
            {
                string componentAppID = ConfigHelper.ComponentAppId;

                string componentAccessToken = componentAccessTokenDAL.Get();
                string url = "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token=" + componentAccessToken;

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息 url", url);

                // 4、使用授权码换取公众号的接口调用凭据和授权信息
                AuthorizationInfoGetReq req = new AuthorizationInfoGetReq();
                req.ComponentAppId = componentAppID;
                req.AuthorizationCode = authCode;
                string requestBody = JsonConvert.SerializeObject(req);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息 requestBody", requestBody);

                string responseBody = HttpHelper.Post(url, requestBody);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息 responseBody", responseBody);

                AuthorizationInfoGetResp resp = JsonConvert.DeserializeObject<AuthorizationInfoGetResp>(responseBody);

                return Authorize(componentAppID, resp.AuthorizationInfo.AuthorizerAppID, componentAccessToken, userID);
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new RESTfulModel() { Code = (int)CodeEnum.系统异常, Msg = codeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }

        private RESTfulModel Authorize(string componentAppID, string authorizerAppID, string componentAccessToken, string userID)
        {
            // 二次授权成功
            AuthorizerInfoGetReq req = new AuthorizerInfoGetReq();
            req.ComponentAppID = componentAppID;
            req.AuthorizerAppID = authorizerAppID;
            string requestBody = JsonConvert.SerializeObject(req);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息 requestBody", requestBody);

            string responseBody = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?component_access_token=" + componentAccessToken, requestBody);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息 responseBody", responseBody);

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
