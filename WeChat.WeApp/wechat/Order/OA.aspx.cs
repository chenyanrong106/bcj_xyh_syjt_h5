using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class OA : WeiPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["oa"] == null && Request.QueryString["ph"] == null)
                {
                    if (Request.QueryString["beforeurl"] != null && Session["beforeurl"] == null)
                        Session["beforeurl"] = Request.QueryString["beforeurl"];
                    HttpCookie cookie = Request.Cookies["cookiedtgohome"];
                    if (cookie == null)
                    {
                        cookie = new HttpCookie("cookiedtgohome");
                        cookie.Expires = DateTime.Now.AddDays(3);
                        cookie.Name = "cookiedtgohome";
                        cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                        HttpContext.Current.Response.Cookies.Add(cookie);
                        BaseLoad2();
                    }
                    else
                    {
                        DateTime dt = DateTime.Parse(cookie.Value);
                        if (dt < DateTime.Now)
                        {
                            cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                            cookie.Expires = DateTime.Now.AddDays(3);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                            BaseLoad2();
                        }
                        else
                        {
                            BaseLoad();
                        }
                    }
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                        string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        Session["FromUserName"] = user;
                        Session["ToUserName"] = user2;
                        if (Session["beforeurl"] != null)
                            Response.Redirect(Session["beforeurl"].ToString().Replace("*", "&"), true);
                        else
                            Response.Redirect("chou.aspx", true);
                    }
                }
                else if (Request.QueryString["oa"] != null)
                {
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        Response.Write("{\"st\":0}");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("{\"st\":1,\"url\":\"oa.aspx\"}");
                        Response.End();
                    }
                }
                else if (Request.QueryString["ph"] != null)
                {
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                        string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        Session["FromUserName"] = user;
                        Session["ToUserName"] = user2;
                        OAauth_Log oa = new MySmallShopService().GetOA(user);
                        if (oa != null && string.IsNullOrEmpty(oa.Phone))
                        {
                            Response.Write("{\"st\":1,\"url\":\"oa.aspx\"}");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("{\"st\":0}");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("{\"st\":1,\"url\":\"oa.aspx\"}");
                        Response.End();
                    }
                }

            }
        }
    }
}