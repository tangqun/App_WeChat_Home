﻿using Helper_9H;
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
        public List<AuthorizationInfoModel> GetRefreshList()
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
                        WHERE TIMESTAMPDIFF(SECOND, refresh_time, NOW()) + 600 >= expires_in;";
            DataTable dt = MySqlHelper.ExecuteDataset(ConfigHelper.ConnStr, sql).Tables[0];
            return EntityListToModelList(dt);
        }

        public AuthorizationInfoModel GetModel(string authorizer_appid)
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
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@authorizer_appid", authorizer_appid));
            return EntityToModel(dr);
        }

        public int Insert(string authorizer_appid, string authorizer_access_token_old, string authorizer_access_token, int expires_in, string authorizer_refresh_token, DateTime auth_time)
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
                                              new MySqlParameter("@authorizer_appid", authorizer_appid),
                                              new MySqlParameter("@authorizer_access_token_old", authorizer_access_token_old),
                                              new MySqlParameter("@authorizer_access_token", authorizer_access_token),
                                              new MySqlParameter("@expires_in", expires_in),
                                              new MySqlParameter("@authorizer_refresh_token", authorizer_refresh_token),
                                              new MySqlParameter("@auth_time", auth_time),
                                              new MySqlParameter("@refresh_time", auth_time),
                                              new MySqlParameter("@create_time", auth_time),
                                              new MySqlParameter("@update_time", auth_time)
                                          };
            return MySqlHelper.ExecuteScalar(ConfigHelper.ConnStr, sql, parameters).ToInt();
        }

        public bool Update(string authorizer_appid, string authorizer_access_token_old, string authorizer_access_token, int expires_in, string authorizer_refresh_token, DateTime auth_time)
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
                                              new MySqlParameter("@authorizer_access_token_old", authorizer_access_token_old),
                                              new MySqlParameter("@authorizer_access_token", authorizer_access_token),
                                              new MySqlParameter("@expires_in", expires_in),
                                              new MySqlParameter("@authorizer_refresh_token", authorizer_refresh_token),
                                              new MySqlParameter("@auth_time", auth_time),
                                              new MySqlParameter("@refresh_time", auth_time),
                                              new MySqlParameter("@update_time", auth_time),
                                              new MySqlParameter("@authorizer_appid", authorizer_appid)
                                          };
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, parameters) > 0;
        }

        public bool Refresh(string authorizer_appid, string authorizer_access_token_old, string authorizer_access_token, int expires_in, string authorizer_refresh_token, DateTime refresh_time)
        {
            string sql =
                        @"UPDATE `authorization_info`
                        SET `authorizer_access_token_old` = @authorizer_access_token_old,
                            `authorizer_access_token` = @authorizer_access_token,
                            `expires_in` = @expires_in,
                            `authorizer_refresh_token` = @authorizer_refresh_token,
                            `refresh_time` = @refresh_time,
                            `update_time` = @update_time
                        WHERE `authorizer_appid` = @authorizer_appid;";
            MySqlParameter[] parameters = {
                                              new MySqlParameter("@authorizer_access_token_old", authorizer_access_token_old),
                                              new MySqlParameter("@authorizer_access_token", authorizer_access_token),
                                              new MySqlParameter("@expires_in", expires_in),
                                              new MySqlParameter("@authorizer_refresh_token", authorizer_refresh_token),
                                              new MySqlParameter("@refresh_time", refresh_time),
                                              new MySqlParameter("@update_time", refresh_time),
                                              new MySqlParameter("@authorizer_appid", authorizer_appid)
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
                model.Id = dr["id"].ToInt();
                model.Authorizer_Appid = dr["authorizer_appid"].ToString();
                model.Authorizer_Access_Token_Old = dr["authorizer_access_token_old"].ToString();
                model.Authorizer_Access_Token = dr["authorizer_access_token"].ToString();
                model.Expires_In = dr["expires_in"].ToInt();
                model.Authorizer_Refresh_Token = dr["authorizer_refresh_token"].ToString();
                model.Refresh_Time = dr["refresh_time"].ToDateTime();
                model.Create_Time = dr["create_time"].ToDateTime();
                model.Update_Time = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
