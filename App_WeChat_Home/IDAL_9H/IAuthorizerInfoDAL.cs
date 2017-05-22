using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IAuthorizerInfoDAL
    {
        int Insert(int user_id, string authorizer_appid, string nick_name, string head_img, int service_type_info, int verify_type_info, string user_name, string alias, string qrcode_url, int open_pay, int open_shake, int open_scan, int open_card, int open_store, int idc, string principal_name, DateTime dt);
        bool Update(string authorizer_appid, string nick_name, string head_img, int service_type_info, int verify_type_info, string alias, string qrcode_url, int open_pay, int open_shake, int open_scan, int open_card, int open_store, int idc, string principal_name, DateTime dt);
        AuthorizerInfoModel GetModel(string authorizer_appid);
        List<AuthorizerInfoModel> GetList(int user_id);
        List<AuthorizerInfoModel> GetList();
    }
}
