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
    public class AuthorizerInfoDAL : IAuthorizerInfoDAL
    {
        public int Insert(int user_id, string authorizer_appid, string nick_name, string head_img, int service_type_info, int verify_type_info, string user_name, string alias, string qrcode_url, int open_pay, int open_shake, int open_scan, int open_card, int open_store, int idc, string principal_name, DateTime dt)
        {
            string sql =
                        @"INSERT INTO `authorizer_info`
                                    (`user_id`,
                                     `authorizer_appid`,
                                     `nick_name`,
                                     `head_img`,
                                     `service_type_info`,
                                     `verify_type_info`,
                                     `user_name`,
                                     `alias`,
                                     `qrcode_url`,
                                     `open_pay`,
                                     `open_shake`,
                                     `open_scan`,
                                     `open_card`,
                                     `open_store`,
                                     `idc`,
                                     `principal_name`,
                                     `create_time`,
                                     `update_time`)
                        VALUES (@user_id,
                                @authorizer_appid,
                                @nick_name,
                                @head_img,
                                @service_type_info,
                                @verify_type_info,
                                @user_name,
                                @alias,
                                @qrcode_url,
                                @open_pay,
                                @open_shake,
                                @open_scan,
                                @open_card,
                                @open_store,
                                @idc,
                                @principal_name,
                                @create_time,
                                @update_time);
                        SELECT @@IDENTITY;";
            MySqlParameter[] parameters = { 
                                              new MySqlParameter("@user_id", user_id),
                                              new MySqlParameter("@authorizer_appid", authorizer_appid),
                                              new MySqlParameter("@nick_name", nick_name),
                                              new MySqlParameter("@head_img", head_img),
                                              new MySqlParameter("@service_type_info", service_type_info),
                                              new MySqlParameter("@verify_type_info", verify_type_info),
                                              new MySqlParameter("@user_name", user_name),
                                              new MySqlParameter("@alias", alias),
                                              new MySqlParameter("@qrcode_url", qrcode_url),
                                              new MySqlParameter("@open_pay", open_pay),
                                              new MySqlParameter("@open_shake", open_shake),
                                              new MySqlParameter("@open_scan", open_scan),
                                              new MySqlParameter("@open_card", open_card),
                                              new MySqlParameter("@open_store", open_store),
                                              new MySqlParameter("@idc", idc),
                                              new MySqlParameter("@principal_name", principal_name),
                                              new MySqlParameter("@create_time", dt),
                                              new MySqlParameter("@update_time", dt)
                                          };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters).ToInt();
        }

        public bool Update(string authorizer_appid, string nick_name, string head_img, int service_type_info, int verify_type_info, string alias, string qrcode_url, int open_pay, int open_shake, int open_scan, int open_card, int open_store, int idc, string principal_name, DateTime dt)
        {
            string sql =
                        @"UPDATE `authorizer_info`
                        SET `nick_name` = @nick_name,
                            `head_img` = @head_img,
                            `service_type_info` = @service_type_info,
                            `verify_type_info` = @verify_type_info,
                            `alias` = @alias,
                            `qrcode_url` = @qrcode_url,
                            `open_pay` = @open_pay,
                            `open_shake` = @open_shake,
                            `open_scan` = @open_scan,
                            `open_card` = @open_card,
                            `open_store` = @open_store,
                            `idc` = @idc,
                            `principal_name` = @principal_name,
                            `update_time` = @update_time
                        WHERE `authorizer_appid` = @authorizer_appid;";
            MySqlParameter[] parameters = { 
                                              new MySqlParameter("@nick_name", nick_name),
                                              new MySqlParameter("@head_img", head_img),
                                              new MySqlParameter("@service_type_info", service_type_info),
                                              new MySqlParameter("@verify_type_info", verify_type_info),
                                              new MySqlParameter("@alias", alias),
                                              new MySqlParameter("@qrcode_url", qrcode_url),
                                              new MySqlParameter("@open_pay", open_pay),
                                              new MySqlParameter("@open_shake", open_shake),
                                              new MySqlParameter("@open_scan", open_scan),
                                              new MySqlParameter("@open_card", open_card),
                                              new MySqlParameter("@open_store", open_store),
                                              new MySqlParameter("@idc", idc),
                                              new MySqlParameter("@principal_name", principal_name),
                                              new MySqlParameter("@authorizer_appid", authorizer_appid),
                                              new MySqlParameter("@update_time", dt)
                                          };
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, parameters) > 0;
        }

        public AuthorizerInfoModel GetModel(string authorizer_appid)
        {
            string sql =
                        @"SELECT
                            `id`,
                            `user_id`,
                            `authorizer_appid`,
                            `nick_name`,
                            `head_img`,
                            `service_type_info`,
                            `verify_type_info`,
                            `user_name`,
                            `alias`,
                            `qrcode_url`,
                            `open_pay`,
                            `open_shake`,
                            `open_scan`,
                            `open_card`,
                            `open_store`,
                            `idc`,
                            `principal_name`,
                            `create_time`,
                            `update_time`
                        FROM `authorizer_info`
                        WHERE `authorizer_appid` = @authorizer_appid LIMIT 0, 1;";
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizer_appid));
            return EntityToModel(dr);
        }

        public List<AuthorizerInfoModel> GetList(int user_id)
        {
            string sql =
                        @"SELECT
                            `id`,
                            `user_id`,
                            `authorizer_appid`,
                            `nick_name`,
                            `head_img`,
                            `service_type_info`,
                            `verify_type_info`,
                            `user_name`,
                            `alias`,
                            `qrcode_url`,
                            `open_pay`,
                            `open_shake`,
                            `open_scan`,
                            `open_card`,
                            `open_store`,
                            `idc`,
                            `principal_name`,
                            `create_time`,
                            `update_time`
                        FROM `authorizer_info`
                        WHERE `user_id` = @user_id";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, new MySqlParameter("@user_id", user_id)).Tables[0];
            return EntityListToModelList(dt);
        }

        public List<AuthorizerInfoModel> GetList()
        {
            string sql =
                        @"SELECT
                            `id`,
                            `user_id`,
                            `authorizer_appid`,
                            `nick_name`,
                            `head_img`,
                            `service_type_info`,
                            `verify_type_info`,
                            `user_name`,
                            `alias`,
                            `qrcode_url`,
                            `open_pay`,
                            `open_shake`,
                            `open_scan`,
                            `open_card`,
                            `open_store`,
                            `idc`,
                            `principal_name`,
                            `create_time`,
                            `update_time`
                        FROM `authorizer_info`";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql).Tables[0];
            return EntityListToModelList(dt);
        }

        private List<AuthorizerInfoModel> EntityListToModelList(DataTable dt)
        {
            List<AuthorizerInfoModel> modelList = new List<AuthorizerInfoModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    modelList.Add(EntityToModel(dr));
                }
            }
            return modelList;
        }

        private AuthorizerInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                AuthorizerInfoModel model = new AuthorizerInfoModel();
                model.Id = dr["id"].ToInt();
                model.User_Id = dr["user_id"].ToInt();
                model.Authorizer_AppId = dr["authorizer_appid"].ToString();
                model.Nick_Name = dr["nick_name"].ToString();
                model.Head_Img = dr["head_img"].ToString();
                model.Service_Type_Info = dr["service_type_info"].ToInt();
                model.Verify_Type_Info = dr["verify_type_info"].ToInt();
                model.User_Name = dr["user_name"].ToString();
                model.Alias = dr["alias"].ToString();
                model.Qrcode_Url = dr["qrcode_url"].ToString();
                model.Open_Pay = dr["open_pay"].ToInt();
                model.Open_Shake = dr["open_shake"].ToInt();
                model.Open_Scan = dr["open_scan"].ToInt();
                model.Open_Card = dr["open_card"].ToInt();
                model.Open_Store = dr["open_store"].ToInt();
                model.IDC = dr["idc"].ToInt();
                model.Principal_Name = dr["principal_name"].ToString();
                model.Create_Time = dr["create_time"].ToDateTime();
                model.Update_Time = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
