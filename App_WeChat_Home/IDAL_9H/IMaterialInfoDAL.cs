using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IMaterialInfoDAL
    {
        int GetCount(string authorizerAppID, string type);
        List<MaterialInfoModel> GetPageList(string authorizerAppID, string type, int pageIndex, int pageSize, out int totalCount);
    }
}
