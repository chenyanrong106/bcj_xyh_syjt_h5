using SPACRM.Business.WXService;
using SPACRM.WebApp.wcf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPACRM.WebApp.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            IWechatCRMService s = new WechatCRMService();
            var a = s.PostMessageByID(18505, "哈哈哈");
            return View();
        }


    }
}
