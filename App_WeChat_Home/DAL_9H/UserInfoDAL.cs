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
    public class UserInfoDAL: IUserInfoDAL
    {
        public UserInfoModel GetModel(string mobile)
        {
            string sql = "SELECT * FROM `user_info` WHERE mobile = @mobile;";
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@mobile", mobile));
            return EntityToModel(dr);
        }

        public bool UpdateToken(string bID, string token, DateTime dt)
        {
            string sql = "UPDATE `user_info` SET token = @token, update_time = @update_time WHERE business_id = @b_id;";
            MySqlParameter[] parameters = {
                new MySqlParameter("@token", token),
                new MySqlParameter("@update_time", dt),
                new MySqlParameter("@b_id", bID),
            };
            return MySqlHelper.ExecuteNonQuery(ConfigHelper.ConnStr, sql, parameters.ToArray()) > 0;
        }

        public UserInfoModel GetByToken(string token)
        {
            string sql = "SELECT * FROM `user_info` WHERE token = @token;";
            DataRow dr = MySqlHelper.ExecuteDataRow(ConfigHelper.ConnStr, sql, new MySqlParameter("@token", token));
            return EntityToModel(dr);
        }

        private UserInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                UserInfoModel model = new UserInfoModel();
                model.ID = dr["id"].ToInt();
                model.BusinessID = dr["business_id"].ToString();
                model.Mobile = dr["mobile"].ToString();
                model.Salt = dr["salt"].ToString();
                model.Password = dr["password"].ToString();
                model.RealName = dr["real_name"].ToString();
                model.UserStat = dr["user_stat"].ToInt();
                model.LoginErrorTimes = dr["login_error_times"].ToInt();
                model.Token = dr["token"].ToString();
                model.CreateTime = dr["create_time"].ToDateTime();
                model.UpdateTime = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
