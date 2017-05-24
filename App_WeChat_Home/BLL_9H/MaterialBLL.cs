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

        // 与腾讯服务器交换信息
        private string GetCount(string authorizerAppID, string type)
        {
            try
            {
                string url = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token=" + accessTokenDAL.Get(authorizerAppID);

                string responseBody = HttpHelper.Get(url);

                LogHelper.Info("获取素材总数" + "\r\n\r\nresponseBody: " + responseBody);

                //int count = 0;
                //switch (type)
                //{
                //    case "voice_count": count = resp.VoiceCount; break;
                //    case "video_count": count = resp.VideoCount; break;
                //    case "image_count": count = resp.ImageCount; break;
                //    case "news_count": count = resp.NewsCount; break;
                //}

                return responseBody;
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return null;
            }
        }

        private string BatchGet(string authorizerAppID, string type, int offset, int count)
        {
            try
            {
                string url = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token=" + accessTokenDAL.Get(authorizerAppID);

                MaterialListGetReq req = new MaterialListGetReq();
                req.Type = type;
                req.Offset = offset;
                req.Count = count;
                string requestBody = JsonConvert.SerializeObject(req);
                
                LogHelper.Info("获取素材列表" + "\r\n\r\nrequestBody: " + requestBody);

                string responseBody = HttpHelper.Post(url, requestBody);

                LogHelper.Info("获取素材列表" + "\r\n\r\nrequestBody: " + requestBody + "\r\n\r\nresponseBody: " + responseBody);

                return responseBody;
            }
            catch (Exception ex)
            {
                LogHelper.Error("唐群", ex);
                return null;
            }
        }

        public string GetPageList(string authorizerAppID, string type, string key, int pageIndex, int pageSize, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
