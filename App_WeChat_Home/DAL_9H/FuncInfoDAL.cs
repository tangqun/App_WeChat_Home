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
        public List<FuncInfoModel> GetList(string authorizer_appid)
        {
            string sql =
                        @"SELECT
                            `id`,
                            `authorizer_appid`,
                            `funcscope_category_id`
                        FROM `func_info`
                        WHERE `authorizer_appid` = @authorizer_appid;";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizer_appid)).Tables[0];
            return EntityListToModelList(dt);
        }

        public int Insert(string authorizer_appid, int funcscope_category_id)
        {
            string sql =
                        @"INSERT INTO `func_info`
                                    (`authorizer_appid`,
                                     `funcscope_category_id`)
                        VALUES (@authorizer_appid,
                                @funcscope_category_id);
                        SELECT @@IDENTITY;";
            MySqlParameter[] parameters = {
                                              new MySqlParameter("@authorizer_appid", authorizer_appid),
                                              new MySqlParameter("@funcscope_category_id", funcscope_category_id)
                                          };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters).ToInt();
        }

        public bool Delete(string authorizer_appid)
        {
            string sql =
                        @"DELETE
                        FROM `func_info`
                        WHERE `authorizer_appid` = @authorizer_appid;";
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizer_appid)) > 0;
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
                model.Id = dr["id"].ToInt();
                model.Authorizer_AppId = dr["authorizer_appid"].ToString();
                model.Funcscope_Category_Id = dr["funcscope_category_id"].ToInt();
                return model;
            }
            return null;
        }
    }
}
