using IBLL_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_9H;
using IDAL_9H;
using DAL_9H;
using Helper_9H;

namespace BLL_9H
{
    public class AuthorizerInfoBLL : IAuthorizerInfoBLL
    {
        private IAuthorizerInfoDAL authorizerInfoDAL = new AuthorizerInfoDAL();

        public List<AuthorizerInfoModel> GetList(int userID)
        {
            try
            {
                return authorizerInfoDAL.GetList(userID);
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return null;
            }
        }

        public AuthorizerInfoModel GetModel(int id)
        {
            try
            {
                return authorizerInfoDAL.GetModel(id);
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return null;
            }
        }
    }
}
