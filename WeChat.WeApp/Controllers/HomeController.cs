using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SPACRM.Common;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Entity.Jsons;
using SPACRM.Extension;
using SPACRM.Interface;
using SPACRM.WebApp.ViewModels;

namespace WeChat.WebApp.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private ILoginService _lservice;
        private ISystemService _sservice;
        private ICommonService _cservice;
        public HomeController(ILoginService lservice,ISystemService sservice,ICommonService cservice)
        {
            _lservice = lservice;
            _sservice = sservice;
            _cservice = cservice;
        }

        [SPACRM.Extension.WXMyAuthorizeAttribute]
        public ActionResult Index()
        {
            if (WXMyContext.CurrentMerchants.ToUserName == null || WXMyContext.CurrentMerchants.ToUserName == "")
            {
                return Redirect("/SystemSet/Merchant.do");
            }
            return View(new Index_Report_EX { });
        }


        public JsonResult IndexSearch()
        {
            Index_Report_EX report = _sservice.GetWXIndexReport(WXMyContext.CurrentLoginUser.ORG_ID, Request.Form["Dashboard_Start_Date"].ToString(), Request.Form["Dashboard_End_Date"].ToString());
            return Json(report);
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string ORG_NO = form["ORG_NO"];
            string USER_NO = form["USER_NO"];
            string USER_PASS = form["USER_PASS"]; //0,1,2;
            JsonSMsg jmsg = new JsonSMsg();
            var cuUser = _lservice.WXLoginUser(USER_NO, USER_PASS);
            if (cuUser == null)
            {
                jmsg.Status = -1;
                jmsg.Message = "公司代码、用户名或密码有误！";
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginUseInfo"] = cuUser;
                FormsAuthentication.SetAuthCookie(cuUser.ID.ToString(), false);
                jmsg.Status = 1;

                if (System.Web.HttpContext.Current.Session["ReturnUrl"] == null)
                {
                    //jmsg.Data = "/Store/Index.do";
                    jmsg.Data = "/Home/Index.do";
                }
                else
                {
                    jmsg.Data = System.Web.HttpContext.Current.Session["ReturnUrl"].ToString();
                }

                //if(.)
                //var ses = System.Web.HttpContext.Current.Session.Keys.OfType<string>();
                //if (ses != null && ses.Count() > 0 && ses.Contains("ReturnUrl"))
                //{
                //    jmsg.Data = System.Web.HttpContext.Current.Session["ReturnUrl"].ToString();
                //}
                //else
                //{
                //    jmsg.Data = AppConfig.Get("DefalutPage");
                //}
                //jmsg.Data = SPACRM.Extension.MyContext.ReturnUrl;

                //if (string.IsNullOrEmpty(ReturnUrl))
                //    jmsg.Data = "/Store/Index.do";
                //else
                //    jmsg.Data = ReturnUrl;
            }
            return Json(jmsg);
            //if (string.IsNullOrEmpty(employeeid))
            //{
            //    return Content("请先登录!");
            //}
            //Response.Cookies.Clear();
            //FormsAuthentication.SignOut();
            //Session.Abandon();
            //FormsAuthentication.SetAuthCookie(employeeid, false);
            //Response.Cookies.Add(new HttpCookie("USER_LEVEL", level.ToString()));
            //Response.Cookies.Add(new HttpCookie("SUB_BRANCHID", subbranchid.ToString())); 
            //return View();
            //if (string.IsNullOrEmpty(url))
            //{
            //    return RedirectToAction("Index", "Booking");
            //}
            //else
            //{
            //    return Redirect(url);
            //}
            //string baseUrl = "http://" + Request.Url.Authority + Url.Action("Login");
            //bool ret = AuthorizeHelper.Verify(Ticket);
            //if (!ret)
            //{
            //    return Redirect(AuthorizeHelper.GetUAMLoginUrl(baseUrl, ReturnUrl));
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(ReturnUrl))
            //    {
            //        ReturnUrl = Url.Action("Index");
            //    }
            //    return Redirect(ReturnUrl);
            //}
        }

        public ActionResult Login(string ReturnUrl)
        {
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            //Session.Abandon();
            Session.Clear();

            //System.Web.HttpContext.Current.Session["ReturnUrl"] = ReturnUrl;
            if (string.IsNullOrEmpty(ReturnUrl))
            {
                System.Web.HttpContext.Current.Session["ReturnUrl"] = "/Home/Index.do";
            }
            else
            {
                System.Web.HttpContext.Current.Session["ReturnUrl"] = ReturnUrl;
            }
            return View();
            //return Content("请先登录!");
            //string baseUrl = "http://" + Request.Url.Autho   bool ret = AuthorizeHelper.Verify(Ticket);
            //if (!ret)
            //{
            //return Redirect(AuthorizeHelper.GetUAMLoginUrl(baseUrl, ReturnUrl));
            //}
            //else
            //{
            //    if (string.IsNullOrEmpty(ReturnUrl))
            //    {
            //        ReturnUrl = Url.Action("Index");
            //    }
            //    return Redirect(ReturnUrl);
            //}
        }

        public FilePathResult ViewImage(int id)
        {
            if (id == 0)
            {
                string emptyFile = Server.MapPath("~/assets/img/icon/s.gif");
                return new FilePathResult(emptyFile, "image/gif");
            }
            FILES fileEntity = _cservice.GetUploadFile(id);
            if (fileEntity == null)
            {
                string emptyFile = Server.MapPath("~/assets/img/icon/s.gif");
                return new FilePathResult(emptyFile, "image/gif");
            }
            return new FilePathResult(fileEntity.FILE_NAME, fileEntity.CONTENT_TYPE);
        }

        //public ActionResult Logout(string ReturnUrl)
        //{
        //    //Response.Cookies.Clear();
        //    //FormsAuthentication.SignOut();
        //    //Session.Abandon();
        //    //System.Web.HttpContext.Current.Session["ReturnUrl"] = ReturnUrl;
        //    return View();
        //    //return Redirect("/Home/Login.do");//TODO:跳转到登录页
        //    //string baseUrl = "http://" + Request.Url.Authority + Url.Action("Login");
        //    //string returnUrl = Url.Action("Index");
        //    //return Redirect(AuthorizeHelper.GetUAMLogOutUrl(baseUrl, returnUrl));
        //}
        //tcfdScatter(顾客姓名)，tcfdScatterTe(顾客电话)，tcfdPaperId(身份证号), tcfdMemberId(会员编号), tifdReservationID(预约号), tifdRoomId(床位号)
        public ActionResult PostRedirect(int type, string memberid, string idcard, string bid, string phone, string fullname, string cardno, string roomid, string openid)
        {

            /// 1 会员checkin 2 非会员checkin 3 会员详情， 4 储值卡  5 诊疗卡开卡 6 结账 7 查看业务单 8 体验会员开卡


            PostRedirectModel model = new PostRedirectModel();
            model.RedirectType = type;
            model.tcfdCardId = cardno;
            model.tcfdMemberId = memberid;
            model.tcfdPaperId = idcard;
            model.tcfdScatter = HttpUtility.UrlEncode(fullname, System.Text.Encoding.UTF8);
            model.tcfdScatterTel = phone;
            model.tifdReservationID = bid;
            model.tifdRoomId = roomid;
            model.tcfdOpeid = openid;
            if (type <= 0)
            {
                throw new ArgumentOutOfRangeException("参数错误");
            }


            return View(model);
        }

        //检索顾客信息
        [HttpPost]
        public JsonResult SearchMemberInfo(string q)
        {
            JsonSMsg jmsg = new JsonSMsg();
            if(string.IsNullOrEmpty(q))
            {
                jmsg.Status = -1;
                jmsg.Message = "请输入查询条件！";
                return Json(jmsg);
            }
            int cid = _sservice.SearchMemberInfo(q,MyContext.CurrentLoginUser.ORG_ID);
            if (cid>0)
            {
                jmsg.Status = 1;
                jmsg.Data = cid;
            }
            else
            {
                jmsg.Status = -1;
                jmsg.Message = "未检索到相关信息！";
            }
            return Json(jmsg);

        }

        ////未约进列表
        //[HttpPost]
        //public JsonResult BookingList(string storeId)
        //{
        //    JsonSMsg jmsg = new JsonSMsg();
        //    if(string.IsNullOrEmpty(storeId)){
        //        jmsg.Status=-1;
        //        return Json(jmsg);
        //    }
        //    List<CUST_INFO_EX> bookingList = _sservice.QueryBookingList(storeId);
        //    if (bookingList.Count > 0)
        //    {
        //        jmsg.Status = 1;
        //        jmsg.Data = bookingList;
        //    }
        //    return Json(jmsg);
        //}

        ////未约进列表
        //[HttpPost]
        //public JsonResult OrderList(string storeId)
        //{
        //    JsonSMsg jmsg = new JsonSMsg();
        //    if (string.IsNullOrEmpty(storeId))
        //    {
        //        jmsg.Status = -1;
        //        return Json(jmsg);
        //    }
        //    List<ORDER_HEAD_EX> orderList = _sservice.QueryOrderList(storeId);
        //    if (orderList.Count > 0)
        //    {
        //        jmsg.Status = 1;
        //        jmsg.Data = orderList;
        //    }
        //    return Json(jmsg);
        //}
    }
}
