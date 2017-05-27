using BLL_9H;
using IBLL_9H;
using Model_9H;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Web_9H.Areas.App.Controllers
{
    public class MaterialController : AppBaseController
    {
        private IMaterialBLL materialBLL = new MaterialBLL();

        public ActionResult NewsList(string key = "", int pageIndex = 1, int pageSize = 12)
        {
            int totalCount = 0;
            List<MaterialInfoModel> modelList = materialBLL.GetPageList(CurrentAuthorizer.AuthorizerAppID, "news", key, pageIndex, pageSize, out totalCount);
            PagedList<MaterialInfoModel> pagedList = new PagedList<MaterialInfoModel>(modelList, pageIndex, pageSize, totalCount);
            return View(pagedList);
        }

        public ActionResult ImageList(int pageIndex = 1, int pageSize = 10)
        {
            int totalCount = 0;
            List<MaterialInfoModel> modelList = materialBLL.GetPageList(CurrentAuthorizer.AuthorizerAppID, "image", null, pageIndex, pageSize, out totalCount);
            PagedList<MaterialInfoModel> pagedList = new PagedList<MaterialInfoModel>(modelList, pageIndex, pageSize, totalCount);
            return View(pagedList);
        }
    }
}