using IDAL_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_9H;
using MySql.Data.MySqlClient;
using Helper_9H;
using System.Data;

namespace DAL_9H
{
    public class MaterialInfoDAL : IMaterialInfoDAL
    {
        public int GetCount(string authorizerAppID, string type)
        {
            string sql =
                        @"SELECT COUNT(1)
                        FROM `material_info`
                        WHERE `authorizer_id` = @authorizer_id AND `type` = @type;";
            MySqlParameter[] parameters = {
                new MySqlParameter("@authorizer_id", authorizerAppID),
                new MySqlParameter("@type", type)
            };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters.ToArray()).ToInt();
        }

        public List<MaterialInfoModel> GetPageList(string authorizerAppID, string type, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = GetCount(authorizerAppID, type);

            string sql =
                        @"SELECT
                            `id`,
                            `authorizer_id`,
                            `type`,
                            `media_id`,
                            `name`,
                            `url`,
                            `create_time`,
                            `update_time`
                        FROM `material_info`
                        WHERE `authorizer_id` = @authorizer_id AND `type` = @type
                        LIMIT @offset, @count;";
            MySqlParameter[] parameters = {
                new MySqlParameter("@authorizer_id", authorizerAppID),
                new MySqlParameter("@type", type),
                new MySqlParameter("@offset", (pageIndex - 1) * pageSize),
                new MySqlParameter("@count", pageSize)
            };
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, parameters.ToArray()).Tables[0];
            return EntityListToModelList(dt);
        }

        private List<MaterialInfoModel> EntityListToModelList(DataTable dt)
        {
            List<MaterialInfoModel> modelList = new List<MaterialInfoModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    modelList.Add(EntityToModel(dr));
                }
            }
            return modelList;
        }

        private MaterialInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                MaterialInfoModel model = new MaterialInfoModel();
                model.MediaID = dr["media_id"].ToString();
                model.Name = dr["name"] == null ? null : dr["name"].ToString();
                model.Url = dr["url"] == null ? null : dr["url"].ToString();
                model.CreateTime = dr["create_time"].ToDateTime();
                model.UpdateTime = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
