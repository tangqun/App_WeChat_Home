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
        private ICodeMsgDAL codeMsgDAL = new CodeMsgDAL();
        private IComponentAccessTokenDAL componentAccessTokenDAL = new ComponentAccessTokenDAL();
        private IFuncInfoDAL funcInfoDAL = new FuncInfoDAL();
        private IAuthorizerInfoDAL authorizerInfoDAL = new AuthorizerInfoDAL();

        public string GetPreAuthCodeUrl()
        {
            try
            {
                string componentAppId = ConfigHelper.ComponentAppId;

                string componentAccessToken = componentAccessTokenDAL.Get();
                string url_3 = "https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token=" + componentAccessToken;

                LogHelper.Info("3、获取预授权码pre_auth_code url_3", url_3);

                PreAuthCodeGetReq req_3 = new PreAuthCodeGetReq();
                req_3.ComponentAppId = componentAppId;
                string requestBody_3 = JsonConvert.SerializeObject(req_3);

                LogHelper.Info("3、获取预授权码pre_auth_code requestBody_3", requestBody_3);

                string responseBody_3 = HttpHelper.Post(url_3, requestBody_3);

                LogHelper.Info("3、获取预授权码pre_auth_code responseBody_3", responseBody_3);

                PreAuthCodeGetResp resp_3 = JsonConvert.DeserializeObject<PreAuthCodeGetResp>(responseBody_3);

                string redirectUri = ConfigHelper.Domain + "home/recvauth";

                return "https://mp.weixin.qq.com/cgi-bin/componentloginpage?component_appid=" + componentAppId + "&pre_auth_code=" + resp_3.PreAuthCode + "&redirect_uri=" + redirectUri;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                // want ex
                return null;
            }
        }

        public RESTfulModel SaveAuth(string authCode, int expiresIn, string userID)
        {
            try
            {
                DateTime authTime = DateTime.Now;

                #region 4、使用授权码换取公众号的接口调用凭据和授权信息
                string componentAppID = ConfigHelper.ComponentAppId;

                string componentAccessToken = componentAccessTokenDAL.Get();
                string url_4 = "https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token=" + componentAccessToken;

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息 url_4", url_4);

                // 4、使用授权码换取公众号的接口调用凭据和授权信息
                AuthorizationInfoGetReq req_4 = new AuthorizationInfoGetReq();
                req_4.ComponentAppId = componentAppID;
                req_4.AuthorizationCode = authCode;
                string requestBody_4 = JsonConvert.SerializeObject(req_4);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息 requestBody_4", requestBody_4);

                string responseBody_4 = HttpHelper.Post(url_4, requestBody_4);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息 responseBody_4", responseBody_4);

                AuthorizationInfoGetResp resp_4 = JsonConvert.DeserializeObject<AuthorizationInfoGetResp>(responseBody_4);
                #endregion

                #region 调用远程接口保存AccessToken信息
                string url = ConfigHelper.DomainToken + "api/accesstoken/save";

                LogHelper.Info("调用远程接口保存AccessToken信息 url", url);

                SaveAuthModel req = new SaveAuthModel()
                {
                    AuthorizerAppID = resp_4.AuthorizationInfo.AuthorizerAppID,
                    AuthorizerAccessToken = resp_4.AuthorizationInfo.AuthorizerAccessToken,
                    ExpiresIn = resp_4.AuthorizationInfo.ExpiresIn,
                    AuthorizerRefreshToken = resp_4.AuthorizationInfo.AuthorizerRefreshToken,
                    AuthTime = authTime
                };
                string requestBody = JsonConvert.SerializeObject(req);

                LogHelper.Info("调用远程接口保存AccessToken信息 requestBody", requestBody);

                string responseBody = HttpHelper.Post(url, requestBody);

                LogHelper.Info("调用远程接口保存AccessToken信息 responseBody", responseBody);

                RESTfulModel resp = JsonConvert.DeserializeObject<RESTfulModel>(responseBody);
                #endregion

                if (resp.Code == 0)
                {
                    #region 权限
                    // 删除权限
                    funcInfoDAL.Delete(resp_4.AuthorizationInfo.AuthorizerAppID);
                    // 插入权限，不存在空集合
                    List<int> funcscopeCategoryIdList = resp_4.AuthorizationInfo.FuncInfo.Select(o => o.FuncscopeCategory.ID).ToList();
                    foreach (var funcscopeCategoryId in funcscopeCategoryIdList)
                    {
                        funcInfoDAL.Insert(resp_4.AuthorizationInfo.AuthorizerAppID, funcscopeCategoryId);
                    } 
                    #endregion

                    // 保存授权者信息
                    return Authorize(componentAppID, componentAccessToken, resp_4.AuthorizationInfo.AuthorizerAppID, userID, authTime);
                }
                else
                {
                    return new RESTfulModel() { Code = (int)CodeEnum.保存授权信息失败, Msg = codeMsgDAL.GetByCode((int)CodeEnum.保存授权信息失败) };
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new RESTfulModel() { Code = (int)CodeEnum.系统异常, Msg = codeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }

        private RESTfulModel Authorize(string componentAppID, string componentAccessToken, string authorizerAppID, string userID, DateTime authTime)
        {
            #region 6、获取授权方的公众号帐号基本信息
            string url_6 = "https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?component_access_token=" + componentAccessToken;

            LogHelper.Info("6、获取授权方的公众号帐号基本信息 url", url_6);

            // 二次授权成功
            AuthorizerInfoGetReq req_6 = new AuthorizerInfoGetReq()
            {
                ComponentAppID = componentAppID,
                AuthorizerAppID = authorizerAppID
            };
            string requestBody_6 = JsonConvert.SerializeObject(req_6);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息 requestBody_6", requestBody_6);

            string responseBody_6 = HttpHelper.Post(url_6, requestBody_6);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息 responseBody_6", responseBody_6);

            AuthorizerInfoGetResp resp_6 = JsonConvert.DeserializeObject<AuthorizerInfoGetResp>(responseBody_6); 
            #endregion

            AuthorizerInfoModel authorizerInfoModel = authorizerInfoDAL.GetModel(authorizerAppID);
            if (authorizerInfoModel == null)
            {
                authorizerInfoDAL.Insert(
                    userID,
                    authorizerAppID,
                    resp_6.AuthorizerInfo.NickName,
                    resp_6.AuthorizerInfo.HeadImg,
                    resp_6.AuthorizerInfo.ServiceTypeInfo.ID,
                    resp_6.AuthorizerInfo.VerifyTypeInfo.ID,
                    resp_6.AuthorizerInfo.UserName,
                    resp_6.AuthorizerInfo.Alias,
                    resp_6.AuthorizerInfo.QrcodeUrl,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenPay,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenShake,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenScan,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenCard,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenStore,
                    resp_6.AuthorizerInfo.IDC,
                    resp_6.AuthorizerInfo.PrincipalName,
                    authTime);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.成功), "授权成功") };
            }
            else
            {
                if (userID != authorizerInfoModel.UserID)
                {
                    return new RESTfulModel() { Code = (int)CodeEnum.失败, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.失败), "公众帐号已授权绑定，如有帐号争执，请联系客服") };
                }

                authorizerInfoDAL.Update(
                    authorizerAppID,
                    resp_6.AuthorizerInfo.NickName,
                    resp_6.AuthorizerInfo.HeadImg,
                    resp_6.AuthorizerInfo.ServiceTypeInfo.ID,
                    resp_6.AuthorizerInfo.VerifyTypeInfo.ID,
                    resp_6.AuthorizerInfo.Alias,
                    resp_6.AuthorizerInfo.QrcodeUrl,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenPay,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenShake,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenScan,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenCard,
                    resp_6.AuthorizerInfo.BusinessInfo.OpenStore,
                    resp_6.AuthorizerInfo.IDC,
                    resp_6.AuthorizerInfo.PrincipalName,
                    authTime);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.成功), "授权成功") };
            }
        }
    }
}
