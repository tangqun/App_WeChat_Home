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
        public List<AuthorizerInfoModel> GetList(string userID)
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
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql, new MySqlParameter("@user_id", userID)).Tables[0];
            return EntityListToModelList(dt);
        }

        public AuthorizerInfoModel GetModel(string authorizerAppID)
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
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizerAppID));
            return EntityToModel(dr);
        }

        public int Insert(string userID, string authorizerAppID, string nickName, string headImg, int serviceTypeInfo, int verifyTypeInfo, string user_name, string alias, string qrcodeUrl, int openPay, int openShake, int openScan, int openCard, int openStore, int idc, string principalName, DateTime createTime)
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
                                              new MySqlParameter("@user_id", userID),
                                              new MySqlParameter("@authorizer_appid", authorizerAppID),
                                              new MySqlParameter("@nick_name", nickName),
                                              new MySqlParameter("@head_img", headImg),
                                              new MySqlParameter("@service_type_info", serviceTypeInfo),
                                              new MySqlParameter("@verify_type_info", verifyTypeInfo),
                                              new MySqlParameter("@user_name", user_name),
                                              new MySqlParameter("@alias", alias),
                                              new MySqlParameter("@qrcode_url", qrcodeUrl),
                                              new MySqlParameter("@open_pay", openPay),
                                              new MySqlParameter("@open_shake", openShake),
                                              new MySqlParameter("@open_scan", openScan),
                                              new MySqlParameter("@open_card", openCard),
                                              new MySqlParameter("@open_store", openStore),
                                              new MySqlParameter("@idc", idc),
                                              new MySqlParameter("@principal_name", principalName),
                                              new MySqlParameter("@create_time", createTime),
                                              new MySqlParameter("@update_time", createTime)
                                          };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters).ToInt();
        }

        public bool Update(string authorizerAppID, string nickName, string headImg, int serviceTypeInfo, int verifyTypeInfo, string alias, string qrcodeUrl, int openPay, int openShake, int openScan, int openCard, int openStore, int idc, string principalName, DateTime updateTime)
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
                                              new MySqlParameter("@nick_name", nickName),
                                              new MySqlParameter("@head_img", headImg),
                                              new MySqlParameter("@service_type_info", serviceTypeInfo),
                                              new MySqlParameter("@verify_type_info", verifyTypeInfo),
                                              new MySqlParameter("@alias", alias),
                                              new MySqlParameter("@qrcode_url", qrcodeUrl),
                                              new MySqlParameter("@open_pay", openPay),
                                              new MySqlParameter("@open_shake", openShake),
                                              new MySqlParameter("@open_scan", openScan),
                                              new MySqlParameter("@open_card", openCard),
                                              new MySqlParameter("@open_store", openStore),
                                              new MySqlParameter("@idc", idc),
                                              new MySqlParameter("@principal_name", principalName),
                                              new MySqlParameter("@update_time", updateTime),
                                              new MySqlParameter("@authorizer_appid", authorizerAppID)
                                          };
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, parameters) > 0;
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
                model.ID = dr["id"].ToInt();
                model.UserID = dr["user_id"].ToString();
                model.AuthorizerAppID = dr["authorizer_appid"].ToString();
                model.NickName = dr["nick_name"].ToString();
                model.HeadImg = dr["head_img"].ToString();
                model.ServiceTypeInfo = dr["service_type_info"].ToInt();
                model.VerifyTypeInfo = dr["verify_type_info"].ToInt();
                model.UserName = dr["user_name"].ToString();
                model.Alias = dr["alias"].ToString();
                model.QrcodeUrl = dr["qrcode_url"].ToString();
                model.OpenPay = dr["open_pay"].ToInt();
                model.OpenShake = dr["open_shake"].ToInt();
                model.OpenScan = dr["open_scan"].ToInt();
                model.OpenCard = dr["open_card"].ToInt();
                model.OpenStore = dr["open_store"].ToInt();
                model.IDC = dr["idc"].ToInt();
                model.PrincipalName = dr["principal_name"].ToString();
                model.CreateTime = dr["create_time"].ToDateTime();
                model.UpdateTime = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
