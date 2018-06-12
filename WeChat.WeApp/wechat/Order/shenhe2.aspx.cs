using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class shenhe2 : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        //public int ordercount = 0;
        //public decimal orderprice = 0;
        public Pet_JiuZhu_Info j = new Pet_JiuZhu_Info { rc = 0, je = 0,EndTime=DateTime.Now };
        public List<Pet_JiuZhu_News> nlist = new List<Pet_JiuZhu_News>();
        public Pet_JiuZhu_ShenQing s = new Pet_JiuZhu_ShenQing { };
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] == null)
            {
                //HttpCookie cookie = Request.Cookies["cookiedtgohome"];
                //if (cookie == null)
                //{
                //    cookie = new HttpCookie("cookiedtgohome");
                //    cookie.Expires = DateTime.Now.AddDays(3);
                //    cookie.Name = "cookiedtgohome";
                //    cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                //    HttpContext.Current.Response.Cookies.Add(cookie);
                //    BaseLoad2();
                //}
                //else
                //{
                //    DateTime dt = DateTime.Parse(cookie.Value);
                //    if (dt < DateTime.Now)
                //    {
                //        cookie.Value = DateTime.Now.AddDays(3).ToString("yyyy.MM.dd");
                //        cookie.Expires = DateTime.Now.AddDays(3);
                //        HttpContext.Current.Response.Cookies.Add(cookie);
                //        BaseLoad2();
                //    }
                //    else
                //    {
                //        BaseLoad();
                //    }
                //}

                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");

                }
                //ordercount = nvbo.GetOrderCount();
                //orderprice = nvbo.GetOrderTotalPrice();
                try
                {
                    if (Request.QueryString["id"] != null || Request.QueryString["state"] != null)
                    {
                        j = nvbo.GetJiuZhuInfo(int.Parse(Request.QueryString["id"] == null ? Request.QueryString["state"] : Request.QueryString["id"]));
                        s = nvbo.GetJiuZhuShenQing(j.FromUserName);
                        if (j != null)
                        {
                            nlist = nvbo.GetJiuZhuNewsList(j.ID);
                        }
                    }
                    else
                    {
                        Response.Redirect("detail.aspx?id=1");
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("detail.aspx?id=1");
                }
            }
            else
            {
               Pet_JiuZhu_Info j=nvbo.GetJiuZhuInfo(int.Parse(Request.QueryString["iid"]));
               if (j != null && (j.State == 0||j.State==null))
               {
                   j.State = int.Parse(Request.Params["zt"]);
                   try
                   {
                       j.BegTime = DateTime.Parse(Request.Params["begtime"]);
                       j.EndTime = DateTime.Parse(Request.Params["endtime"]);
                   }
                   catch (Exception)
                   {
                       
                   }
                   nvbo.SaveJiuZhuInfo(j);
                   Response.Write("审核成功");
                   Response.End();
               }
                
            }
        }
    }
}