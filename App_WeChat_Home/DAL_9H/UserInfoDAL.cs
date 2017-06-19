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

        private UserInfoModel EntityToModel(DataRow dr)
        {
            if (dr != null)
            {
                UserInfoModel model = new UserInfoModel();
                model.ID = dr["id"].ToInt();
                model.Mobile = dr["mobile"].ToString();
                model.Salt = dr["salt"].ToString();
                model.Password = dr["password"].ToString();
                model.RealName = dr["real_name"].ToString();
                model.UserStat = dr["user_stat"].ToInt();
                model.LoginErrorTimes = dr["login_error_times"].ToInt();
                model.CreateTime = dr["create_time"].ToDateTime();
                model.UpdateTime = dr["update_time"].ToDateTime();
                return model;
            }
            return null;
        }
    }
}
