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
            //int totalCount = 0;
            //List<Material> modelList = materialBLL.GetPageList("", "news", key, pageIndex, pageSize, out totalCount);
            //PagedList<Material> pagedList = new PagedList<Material>(modelList, pageIndex, pageSize, totalCount);
            return View();
        }

        public ActionResult ImageList(int pageIndex = 1, int pageSize = 10)
        {
            return View();
        }

        public ActionResult VoiceList(int pageIndex = 1, int pageSize = 10)
        {
            return View();
        }

        public ActionResult VideoList(int pageIndex = 1, int pageSize = 10)
        {
            return View();
        }
    }
}