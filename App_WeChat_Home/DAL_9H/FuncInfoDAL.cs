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
    public class FuncInfoDAL : IFuncInfoDAL
    {
        public List<FuncInfoModel> GetList(string authorizerAppID)
        {
            string sql =
                        @"SELECT
                            `id`,
                            `authorizer_appid`,
                            `funcscope_category_id`
                        FROM `func_info`
                        WHERE `authorizer_appid` = @authorizer_appid;";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizerAppID)).Tables[0];
            return EntityListToModelList(dt);
        }

        public int Insert(string authorizerAppID, int funcscopeCategoryID)
        {
            string sql =
                        @"INSERT INTO `func_info`
                                    (`authorizer_appid`,
                                     `funcscope_category_id`)
                        VALUES (@authorizer_appid,
                                @funcscope_category_id);
                        SELECT @@IDENTITY;";
            MySqlParameter[] parameters = {
                                              new MySqlParameter("@authorizer_appid", authorizerAppID),
                                              new MySqlParameter("@funcscope_category_id", funcscopeCategoryID)
                                          };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters).ToInt();
        }

        public bool Delete(string authorizerAppID)
        {
            string sql =
                        @"DELETE
                        FROM `func_info`
                        WHERE `authorizer_appid` = @authorizer_appid;";
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizerAppID)) > 0;
        }

        private List<FuncInfoModel> EntityListToModelList(DataTable dt)
        {
            List<FuncInfoModel> modelList = new List<FuncInfoModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    modelList.Add(EntityToModel(dr));
                }
            }
            return modelList;
        }

        private FuncInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                FuncInfoModel model = new FuncInfoModel();
                model.ID = dr["id"].ToInt();
                model.AuthorizerAppID = dr["authorizer_appid"].ToString();
                model.FuncscopeCategoryID = dr["funcscope_category_id"].ToInt();
                return model;
            }
            return null;
        }
    }
}
