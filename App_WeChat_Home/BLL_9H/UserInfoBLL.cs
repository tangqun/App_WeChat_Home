using IBLL_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_9H;
using Helper_9H;
using IDAL_9H;
using DAL_9H;

namespace BLL_9H
{
    public class UserInfoBLL : IUserInfoBLL
    {
        private ICodeMsgDAL codeMsgDAL = new CodeMsgDAL();
        private IUserInfoDAL userInfoDAL = new UserInfoDAL();

        public RESTfulModel Login(LoginReq model)
        {
            try
            {
                // 字段验证

                UserInfoModel userInfoModel = userInfoDAL.GetModel(model.Mobile);
                if (userInfoModel != null)
                {
                    string pwd = EncryptHelper.MD5Encrypt(ConfigHelper.Salt + EncryptHelper.MD5Encrypt(model.Password));

                    if (userInfoModel.Password == pwd)
                    {
                        string token = Guid.NewGuid().ToString().Replace("-", "");
                        // 更新token
                        userInfoDAL.UpdateToken(userInfoModel.BusinessID, token, DateTime.Now);

                        // 记录token日志

                        return new RESTfulModel() { Code = (int)CodeEnum.成功, Msg = string.Format(codeMsgDAL.GetByCode((int)CodeEnum.成功), "登陆成功"), Data = userInfoModel };
                    }
                    else
                    {
                        return new RESTfulModel() { Code = (int)CodeEnum.密码错误, Msg = codeMsgDAL.GetByCode((int)CodeEnum.密码错误) };
                    }
                }
                else
                {
                    return new RESTfulModel() { Code = (int)CodeEnum.账号不存在, Msg = codeMsgDAL.GetByCode((int)CodeEnum.账号不存在) };
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return new RESTfulModel() { Code = (int)CodeEnum.系统异常, Msg = codeMsgDAL.GetByCode((int)CodeEnum.系统异常) };
            }
        }

        public UserInfoModel GetByToken(string token)
        {
            try
            {
                return userInfoDAL.GetByToken(token);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
