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
    public class AuthorizationInfoDAL : IAuthorizationInfoDAL
    {
        public AuthorizationInfoModel GetModel(string authorizerAppID)
        {
            string sql =
                        @"SELECT
                            `id`,
                            `authorizer_appid`,
                            `authorizer_access_token_old`,
                            `authorizer_access_token`,
                            `expires_in`,
                            `authorizer_refresh_token`,
                            `auth_time`,
                            `refresh_time`,
                            `create_time`,
                            `update_time`
                        FROM `authorization_info`
                        WHERE `authorizer_appid` = @authorizer_appid
                        LIMIT 0, 1;";
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizerAppID));
            return EntityToModel(dr);
        }

        public int Insert(string authorizerAppID, string authorizerAccessTokenOld, string authorizerAccessToken, int expiresIn, string authorizerRefreshToken, DateTime authTime)
        {
            string sql =
                        @"INSERT INTO `authorization_info`
                                    (`authorizer_appid`,
                                     `authorizer_access_token_old`,
                                     `authorizer_access_token`,
                                     `expires_in`,
                                     `authorizer_refresh_token`,
                                     `auth_time`,
                                     `refresh_time`,
                                     `create_time`,
                                     `update_time`)
                        VALUES (@authorizer_appid,
                                @authorizer_access_token_old,
                                @authorizer_access_token,
                                @expires_in,
                                @authorizer_refresh_token,
                                @auth_time,
                                @refresh_time,
                                @create_time,
                                @update_time);
                        SELECT @@IDENTITY;";
            MySqlParameter[] parameters = {
                                              new MySqlParameter("@authorizer_appid", authorizerAppID),
                                              new MySqlParameter("@authorizer_access_token_old", authorizerAccessTokenOld),
                                              new MySqlParameter("@authorizer_access_token", authorizerAccessToken),
                                              new MySqlParameter("@expires_in", expiresIn),
                                              new MySqlParameter("@authorizer_refresh_token", authorizerRefreshToken),
                                              new MySqlParameter("@auth_time", authTime),
                                              new MySqlParameter("@refresh_time", authTime),
                                              new MySqlParameter("@create_time", authTime),
                                              new MySqlParameter("@update_time", authTime)
                                          };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters).ToInt();
        }

        public bool Update(string authorizerAppID, string authorizerAccessTokenOld, string authorizerAccessToken, int expiresIn, string authorizerRefreshToken, DateTime authTime)
        {
            string sql =
                        @"UPDATE `authorization_info`
                        SET `authorizer_access_token_old` = @authorizer_access_token_old,
                            `authorizer_access_token` = @authorizer_access_token,
                            `expires_in` = @expires_in,
                            `authorizer_refresh_token` = @authorizer_refresh_token,
                            `auth_time` = @auth_time,
                            `refresh_time` = @refresh_time,
                            `update_time` = @update_time
                        WHERE `authorizer_appid` = @authorizer_appid;";
            MySqlParameter[] parameters = {
                                              new MySqlParameter("@authorizer_access_token_old", authorizerAccessTokenOld),
                                              new MySqlParameter("@authorizer_access_token", authorizerAccessToken),
                                              new MySqlParameter("@expires_in", expiresIn),
                                              new MySqlParameter("@authorizer_refresh_token", authorizerRefreshToken),
                                              new MySqlParameter("@auth_time", authTime), // 二次授权
                                              new MySqlParameter("@refresh_time", authTime),
                                              new MySqlParameter("@update_time", authTime),
                                              new MySqlParameter("@authorizer_appid", authorizerAppID)
                                          };
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, parameters) > 0;
        }

        private List<AuthorizationInfoModel> EntityListToModelList(DataTable dt)
        {
            List<AuthorizationInfoModel> modelList = new List<AuthorizationInfoModel>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    modelList.Add(EntityToModel(dr));
                }
            }
            return modelList;
        }

        private AuthorizationInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                AuthorizationInfoModel model = new AuthorizationInfoModel();
                model.ID = dr["id"].ToInt();
                model.AuthorizerAppID = dr["authorizer_appid"].ToString();
                model.AuthorizerAccessTokenOld = dr["authorizer_access_token_old"].ToString();
                model.AuthorizerAccessToken = dr["authorizer_access_token"].ToString();
                model.ExpiresIn = dr["expires_in"].ToInt();
                model.AuthorizerRefreshToken = dr["authorizer_refresh_token"].ToString();
                model.RefreshTime = dr["refresh_time"].ToDateTime();
                model.CreateTime = dr["create_time"].ToDateTime();
                model.UpdateTime = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
