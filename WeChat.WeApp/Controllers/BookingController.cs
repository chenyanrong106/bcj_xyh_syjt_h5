using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.Jsons;
using SPACRM.Entity.PageSearch;
using SPACRM.Entity.SearchEntity;
using SPACRM.Extension;
using SPACRM.Interface;
using SPACRM.WebApp.POSApiHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChat.WebApp.Controllers
{
    public class BookingController : WXMyControllerBase
    {
        private IStoreService _ss;
        public BookingController(IStoreService ss)
        {
            _ss = ss;
        }

        public ActionResult Index()
        {
            return View();
        }

        [MyAuthorize]
        public ActionResult StoreList()
        {
            return View(new WXStore() { ID = 0 });
        }

        public JsonResult ImportStores()
        {
            StoreApi sa = new StoreApi();
            JsonSMsg rMsg = new JsonSMsg();
            try
            {
                var result = sa.BindStore(7);//值需要获取
                rMsg.Status = 0;
                rMsg.Message = "success";
                rMsg.Data = result;
            }
            catch
            {
                rMsg.Status = -1;
                rMsg.Message = "fail";
            }
            return Json(rMsg);
        }

        [HttpPost]
        public JsonResult QueryStoreList(FormCollection form)
        {
            //string colkey = form["colkey"];
            //string colsinfo = form["colsinfo"];
            //var stores = _ss.GetStores(CurrentMerchants.ID);
            //JsonQTable fdata = JsonQTable.ConvertFromList<WXStore>(stores, colkey, colsinfo.Split(','));
            //return Json(fdata);

            StoreSearch search = new StoreSearch();
            search.ORG_ID = base.CurrentUserOrgId;
            //search.REGION_ID = Int32.Parse(form["REGION_ID1"].ToString());
            //search.STYPE = form["STYPE"].ToString();
            //search.SSTATUS = form["SSTATUS"].ToString();
            //search.SNAME = form["SNAME"].ToString();

            PageView view = new PageView(form);
            string colkey = form["colkey"];
            string colsinfo = form["colsinfo"];
            PagedList<ORG_STORE_EX> pList = _ss.QueryAllStoreDate(search, view);
            JsonQTable fdata = JsonQTable.ConvertFromPagedList<ORG_STORE_EX>(pList, colkey, colsinfo.Split(','));
            return Json(fdata);
        }

        public JsonResult GetStore(int id)
        {
            var store = _ss.GetStore(id);
            JsonSMsg rMsg = new JsonSMsg();
            if (store != null)
            {
                rMsg.Status = 0;
                rMsg.Message = "success";
                rMsg.Data = store;
            }
            else
            {
                rMsg.Status = -1;
                rMsg.Message = "fail";
            }
            return Json(rMsg);
        }

        public JsonResult SaveStore(FormCollection form, WXStore s)
        {
            if (form["IsDisplay"] == "1")
            {
                s.IsDisplay = true;
            }
            else if (form["IsDisplay"] == "0")
            {
                s.IsDisplay = false;
            }

            var id = _ss.UpdateStore(s);
            JsonSMsg rMsg = new JsonSMsg();
            if (id > 0)
            {
                rMsg.Status = 0;
                rMsg.Message = "success";
            }
            else
            {
                rMsg.Status = -1;
                rMsg.Message = "fail";
            }
            return Json(rMsg);
        }
    }
}
