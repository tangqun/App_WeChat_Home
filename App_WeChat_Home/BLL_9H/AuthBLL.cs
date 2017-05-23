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

                Pre_Auth_Code_Req pac_req = new Pre_Auth_Code_Req();
                pac_req.Component_AppId = componentAppId;
                string requestBody_3 = JsonConvert.SerializeObject(pac_req);

                LogHelper.Info("3、获取预授权码pre_auth_code" + "\r\n\r\n" + requestBody_3);

                string responseBody_3 = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/component/api_create_preauthcode?component_access_token=" + componentAccessTokenDAL.Get(), requestBody_3);

                LogHelper.Info("3、获取预授权码pre_auth_code" + "\r\n\r\n" + requestBody_3 + "\r\n\r\n" + responseBody_3);

                if (!string.IsNullOrEmpty(responseBody_3))
                {
                    Pre_Auth_Code_Resp pac_resp = JsonConvert.DeserializeObject<Pre_Auth_Code_Resp>(responseBody_3);
                    if (pac_resp != null)
                    {
                        return pac_resp.Pre_Auth_Code;
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
                string component_appid = ConfigHelper.ComponentAppId;
                //string appSecret = ConfigHelper.AppSecret;
                //string encodingAESKey = ConfigHelper.EncodingAESKey;

                // 4、使用授权码换取公众号的接口调用凭据和授权信息
                AuthorizationInfoGetReq req = new AuthorizationInfoGetReq();
                req.ComponentAppId = component_appid;
                req.AuthorizationCode = authCode;
                string requestBody_4 = JsonConvert.SerializeObject(ai_req);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息" + "\r\n\r\n" + requestBody_4);

                string component_access_token = componentAccessTokenDAL.Get();

                string responseBody_4 = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/component/api_query_auth?component_access_token=" + component_access_token, requestBody_4);

                LogHelper.Info("4、使用授权码换取公众号的接口调用凭据和授权信息" + "\r\n\r\n" + requestBody_4 + "\r\n\r\n" + responseBody_4);

                Authorization_Info_Resp ai_resp = JsonConvert.DeserializeObject<Authorization_Info_Resp>(responseBody_4);
                #region 授权信息存数据库
                // 授权信息存数据库
                AuthorizationInfoModel authorizationInfoModel = authorizationInfoDAL.GetModel(ai_resp.Authorization_Info.Authorizer_AppId);
                if (authorizationInfoModel != null)
                {
                    // 更新
                    bool res = authorizationInfoDAL.Update(
                        ai_resp.Authorization_Info.Authorizer_AppId,
                        authorizationInfoModel.Authorizer_Access_Token,// 当前的置为旧的，用于消息延时
                        ai_resp.Authorization_Info.Authorizer_Access_Token,
                        ai_resp.Authorization_Info.Expires_In,
                        ai_resp.Authorization_Info.Authorizer_Refresh_Token,
                        DateTime.Now);

                    // 删除权限
                    funcInfoDAL.Delete(ai_resp.Authorization_Info.Authorizer_AppId);
                    // 插入权限，不存在空集合
                    List<int> authority_wechat_IdList = ai_resp.Authorization_Info.Func_Info.Select(o => o.Funcscope_Category.Id).ToList();
                    foreach (var authority_wechat_Id in authority_wechat_IdList)
                    {
                        funcInfoDAL.Insert(ai_resp.Authorization_Info.Authorizer_AppId, authority_wechat_Id);
                    }

                    return Authorize(component_appid, ai_resp.Authorization_Info.Authorizer_AppId, component_access_token, user_id);
                }
                else
                {
                    // 插入
                    int id = authorizationInfoDAL.Insert(
                        ai_resp.Authorization_Info.Authorizer_AppId,
                        ai_resp.Authorization_Info.Authorizer_Access_Token,
                        ai_resp.Authorization_Info.Authorizer_Access_Token,
                        ai_resp.Authorization_Info.Expires_In,
                        ai_resp.Authorization_Info.Authorizer_Refresh_Token,
                        DateTime.Now);

                    // 插入权限
                    List<int> authority_wechat_IdList = ai_resp.Authorization_Info.Func_Info.Select(o => o.Funcscope_Category.Id).ToList();
                    foreach (var authority_wechat_Id in authority_wechat_IdList)
                    {
                        funcInfoDAL.Insert(ai_resp.Authorization_Info.Authorizer_AppId, authority_wechat_Id);
                    }

                    // 授权成功
                    return Authorize(component_appid, ai_resp.Authorization_Info.Authorizer_AppId, component_access_token, user_id);
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return new RESTfulModel() { Code = (int)CodeEnum.系统异常, Msg = codeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }

        private RESTfulModel Authorize(string component_appid, string authorizer_appid, string component_access_token, int user_id)
        {
            // 二次授权成功
            Authorizer_Info_Req ari_req = new Authorizer_Info_Req();
            ari_req.Component_AppId = component_appid;
            ari_req.Authorizer_AppId = authorizer_appid;
            string requestBody_6 = JsonConvert.SerializeObject(ari_req);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息" + "\r\n\r\n" + requestBody_6);

            string responseBody_6 = HttpHelper.Post("https://api.weixin.qq.com/cgi-bin/component/api_get_authorizer_info?component_access_token=" + component_access_token, requestBody_6);

            LogHelper.Info("6、获取授权方的公众号帐号基本信息" + "\r\n\r\n" + requestBody_6 + "\r\n\r\n" + responseBody_6);

            Authorizer_Info_Resp ari_resp = JsonConvert.DeserializeObject<Authorizer_Info_Resp>(responseBody_6);

            AuthorizerInfoModel authorizerInfoModel = authorizerInfoDAL.GetModel(authorizer_appid);
            if (authorizerInfoModel != null)
            {
                if (user_id != authorizerInfoModel.User_Id)
                {
                    return new RESTfulModel() { Code = (int)CodeEnum.失败, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.失败), "公众帐号已授权绑定，如有帐号争执，请联系客服") };
                }

                authorizerInfoDAL.Update(
                    authorizer_appid,
                    ari_resp.Authorizer_Info.Nick_Name,
                    ari_resp.Authorizer_Info.Head_Img,
                    ari_resp.Authorizer_Info.Service_Type_Info.Id,
                    ari_resp.Authorizer_Info.Verify_Type_Info.Id,
                    ari_resp.Authorizer_Info.Alias,
                    ari_resp.Authorizer_Info.Qrcode_Url,
                    ari_resp.Authorizer_Info.Business_Info.Open_Pay,
                    ari_resp.Authorizer_Info.Business_Info.Open_Shake,
                    ari_resp.Authorizer_Info.Business_Info.Open_Scan,
                    ari_resp.Authorizer_Info.Business_Info.Open_Card,
                    ari_resp.Authorizer_Info.Business_Info.Open_Store,
                    ari_resp.Authorizer_Info.IDC,
                    ari_resp.Authorizer_Info.Principal_Name,
                    DateTime.Now);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.失败), "授权成功") };
            }
            else
            {
                authorizerInfoDAL.Insert(
                    user_id,
                    authorizer_appid,
                    ari_resp.Authorizer_Info.Nick_Name,
                    ari_resp.Authorizer_Info.Head_Img,
                    ari_resp.Authorizer_Info.Service_Type_Info.Id,
                    ari_resp.Authorizer_Info.Verify_Type_Info.Id,
                    ari_resp.Authorizer_Info.User_Name,
                    ari_resp.Authorizer_Info.Alias,
                    ari_resp.Authorizer_Info.Qrcode_Url,
                    ari_resp.Authorizer_Info.Business_Info.Open_Pay,
                    ari_resp.Authorizer_Info.Business_Info.Open_Shake,
                    ari_resp.Authorizer_Info.Business_Info.Open_Scan,
                    ari_resp.Authorizer_Info.Business_Info.Open_Card,
                    ari_resp.Authorizer_Info.Business_Info.Open_Store,
                    ari_resp.Authorizer_Info.IDC,
                    ari_resp.Authorizer_Info.Principal_Name, 
                    DateTime.Now);

                return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.失败), "授权成功") };
            }
        }
    }
}
