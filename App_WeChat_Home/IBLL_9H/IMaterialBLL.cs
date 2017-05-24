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
        int GetCount(string authorizerAppID, string type);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizerAppID"></param>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）、图文（news）</param>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<Material> GetPageList(string authorizerAppID, string type, string key, int pageIndex, int pageSize, out int totalCount);
    }
}
