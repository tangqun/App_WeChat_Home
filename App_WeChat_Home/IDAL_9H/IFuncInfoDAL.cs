using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IFuncInfoDAL
    {
        List<FuncInfoModel> GetList(string authorizer_appid);
        int Insert(string authorizer_appid, int funcscope_category_id);
        bool Delete(string authorizer_appid);
    }
}
