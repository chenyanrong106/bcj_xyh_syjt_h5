using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Entity.Entities;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class wo : WeiPage
    {
        MySmallShopService mss = new MySmallShopService();
        NewVerService nvbo = new NewVerService();
        public OAauth_Log oa = null;
        public int myzc = 0; //我的支持
        public decimal mycl = 0;  //我的筹粮
        public decimal myck = 0;  //我的筹款
        public int mypost = 0; //我发起的
        public int mystar = 0; //我关注的
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
            {
                string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                Session["FromUserName"] = user;
                Session["ToUserName"] = user2;

            }
            if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
            {
                string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                if (Request.QueryString["para"] == null)
                {
                    oa = mss.GetOA(user);//根据微信号获取到的信息
                    myzc = nvbo.GetMyJiuZhuCount(user);
                    mycl = nvbo.GetMyJiuZhuMoney(user, 1);
                    myck = nvbo.GetMyJiuZhuMoney(user, 2);
                    mypost = nvbo.GetMyPostJiuZhuCount(user);
                    mystar = nvbo.GetMyStarJiuZhuCount(user);
                }
                else
                {
                    string html = "";
                    List<MyJiuZhu> mlist = nvbo.GetMyJiuZhuList(user, int.Parse(Request.Params["jztype"]), int.Parse(Request.Params["page"]));
                    foreach (var m in mlist)
                    {
                        if (Request.Params["jztype"] == "1")
                        {
                            html += string.Format(@"<li><a href=""detail.aspx?id={4}&p={5}"">
                    <div class=""imgbox"">
                        <img src=""{0}""></div>
                    <div class=""jiluinfo"">
                        <p><strong>{1}</strong></p>
                        <p>所捐狗粮：<bdo>{2}</bdo>kg</p>
                        <p>捐款次数：<bdo>{3}</bdo>次</p>
                    </div>
                </a>
                </li>", WebUrl + m.CImg, m.NickName, Math.Round(m.je / 16, 2), m.cs, m.ID, Request.Params["p"]);
                        }
                        else
                        {
                            html += string.Format(@"<li><a href=""detail.aspx?id={4}&p={5}"">
                    <div class=""imgbox"">
                        <img src=""{0}""></div>
                    <div class=""jiluinfo"">
                        <p><strong>{1}</strong></p>
                        <p>所捐狗粮：<bdo>{2}</bdo>元</p>
                        <p>捐款次数：<bdo>{3}</bdo>次</p>
                    </div>
                </a>
                </li>", WebUrl + m.CImg, m.NickName, m.je, m.cs, m.ID, Request.Params["p"]);
                        }
                    }
                    Response.Write(html);
                    Response.End();
                }
            }
        }
    }
}