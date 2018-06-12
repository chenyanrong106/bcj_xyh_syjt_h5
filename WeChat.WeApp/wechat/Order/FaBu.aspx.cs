using SPACRM.Business.ServiceImpl;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class FaBu : WeiPage
    {
        MySmallShopService mss = new MySmallShopService();
        NewVerService nvbo = new NewVerService();
        //public OAauth_Log oa = null;
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
                    //oa = mss.GetOA(user);//根据微信号获取到的信息
                }
                else
                {

                    string html = "";
                    List<MyJiuZhu> mlist = nvbo.GetMyPostJiuZhuList(user, int.Parse(Request.Params["type"]), int.Parse(Request.Params["page"]));
                    foreach (var m in mlist)
                    {
                        html += string.Format(@"<li><a href=""detail.aspx?id={0}"">
                        <div class=""imgbox"">
                            <img src=""{1}"">
                        </div>
                        <div class=""jiluinfo"">
                            <p><strong>{2}</strong></p>
                            <p><bdo>剩余{4}天</bdo></p>
                            <p>结束日期：{3}</p>
                        </div>
                    </a>
                    </li>", m.ID, WebUrl + m.CImg, m.NickName, m.EndTime.ToString("yyyy.MM.dd"), m.EndTime < DateTime.Now ? 0 : (m.EndTime - DateTime.Now).Days + 1);
                    }
                    Response.Write(html);
                    Response.End();
                }
            }
        }
    }
}