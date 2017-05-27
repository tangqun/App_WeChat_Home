using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL_9H
{
    public interface IMaterialBLL
    {
        List<MaterialInfoModel> GetPageList(string authorizerAppID, string type, string key, int pageIndex, int pageSize, out int totalCount);
    }
}
