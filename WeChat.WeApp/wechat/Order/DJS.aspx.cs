using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class DJS : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                //{
                //    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                //    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                //    Session["FromUserName"] = user;
                //    Session["ToUserName"] = user2;
                //}

                BaseLoad();

                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
                    int num = nvbo.GetYHQCount();
                    yhqsl.InnerHtml = "剩余" + ((4800 - num) < 0 ? 0 : (4800 - num)) + "张";
                    if (num >= 4800)
                    {
                        lq.Visible = false;
                        lw.Visible = true;
                    }
                }
                if (Request.Params["para"] != null)
                {
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                        string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        int num = nvbo.GetYHQCount();
                        if (num >= 4800)
                        {
                            Response.Write("{\"st\":\"2\",\"msg\":\"本次优惠券已领完，<br>敬请期待下次活动。\"}");
                        }
                        else
                        {
                           
                        }
                        Response.End();
                    }
                    else
                    {
                        Response.Write("{\"st\":\"1\",\"msg\":\"未获取到用户信息，<br>可尝试清理微信缓存后再试。\"}");
                        Response.End();
                    }
                }
            }
        }
    }
}