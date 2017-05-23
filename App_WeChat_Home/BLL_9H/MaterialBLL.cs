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
    public class MaterialBLL: IMaterialBLL
    {
        private IAccessTokenDAL accessTokenDAL = new AccessTokenDAL();

        public int GetCount(string appID, string type)
        {
            try
            {
                string materialCountGetUrl = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token=" + accessTokenDAL.Get(appID);

                LogHelper.Info("materialCountGetUrl: " + materialCountGetUrl);

                string materialCountGetResp = HttpHelper.Get(materialCountGetUrl);

                LogHelper.Info("materialCountGetResp: " + materialCountGetResp);

                MaterialCountGetResp resp = JsonConvert.DeserializeObject<MaterialCountGetResp>(materialCountGetResp);

                int count = 0;
                switch (type)
                {
                    case "voice_count": count = resp.VoiceCount; break;
                    case "video_count": count = resp.VideoCount; break;
                    case "image_count": count = resp.ImageCount; break;
                    case "news_count": count = resp.NewsCount; break;
                }

                return count;
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return 0;
            }
        }

        public List<Material> GetPageList(string appID, string type, string key, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = 0;
            try
            {
                string materialListGetUrl = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token=" + accessTokenDAL.Get(appID);

                LogHelper.Info("materialListGetUrl: " + materialListGetUrl);

                MaterialListGetReq req = new MaterialListGetReq();
                req.Type = type;
                req.Offset = (pageIndex - 1) * pageSize;
                req.Count = pageSize;
                string requestBody = JsonConvert.SerializeObject(req);

                string materialListGetResp = HttpHelper.Post(materialListGetUrl, requestBody);
                MaterialListGetResp resp = JsonConvert.DeserializeObject<MaterialListGetResp>(materialListGetResp);

                totalCount = resp.ItemCount;
                return resp.Item;
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return null;
            }
        }
    }
}
