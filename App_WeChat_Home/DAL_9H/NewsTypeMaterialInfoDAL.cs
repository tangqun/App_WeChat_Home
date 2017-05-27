using Helper_9H;
using IDAL_9H;
using Model_9H;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_9H
{
    public class NewsTypeMaterialInfoDAL: INewsTypeMaterialInfoDAL
    {
        public List<NewsTypeMaterialInfoModel> GetList(string authorizerAppID, string key)
        {
            string sql =
                        @"SELECT DISTINCT media_id, `title`, `thumb_media_id`, `show_cover_pic`, `author`, `digest`, `content`, `url`, `content_source_url` FROM `newstypematerial_info` 
                        WHERE authorizer_id = @authorizer_id AND (title LIKE @title OR author LIKE @author OR content LIKE @content) 
                        ORDER BY `update_time` DESC, id DESC;";
            MySqlParameter[] parameters = {
                new MySqlParameter("@authorizer_id", authorizerAppID),
                new MySqlParameter("@title", "%" + key + "%"),
                new MySqlParameter("@author", "%" + key + "%"),
                new MySqlParameter("@content", "%" + key + "%")
            };
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, parameters.ToArray()).Tables[0];
            return EntityListToModelList(dt);
        }

        public List<NewsTypeMaterialInfoModel> GetList(string mediaID)
        {
            string sql =
                        @"SELECT
                            `id`,
                            `authorizer_id`,
                            `media_id`,
                            `title`,
                            `thumb_media_id`,
                            `show_cover_pic`,
                            `author`,
                            `digest`,
                            `content`,
                            `url`,
                            `content_source_url`
                        FROM `newstypematerial_info`
                        WHERE `media_id` = @media_id;";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, new MySqlParameter("@media_id", mediaID)).Tables[0];
            return EntityListToModelList(dt);
        }

        private List<NewsTypeMaterialInfoModel> EntityListToModelList(DataTable dt)
        {
            List<NewsTypeMaterialInfoModel> modelList = new List<NewsTypeMaterialInfoModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    modelList.Add(EntityToModel(dr));
                }
            }
            return modelList;
        }

        private NewsTypeMaterialInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                NewsTypeMaterialInfoModel model = new NewsTypeMaterialInfoModel();
                model.MediaID = dr["media_id"].ToString();
                model.Title = dr["title"].ToString();
                model.ThumbMediaID = dr["thumb_media_id"].ToString();
                model.ShowCoverPic = dr["show_cover_pic"].ToInt();
                model.Author = dr["author"].ToString();
                model.Digest = dr["digest"].ToString();
                model.Content = dr["content"].ToString();
                model.Url = dr["url"].ToString();
                model.ContentSourceUrl = dr["content_source_url"].ToString();
                return model;
            }
            return null;
        }
    }
}
