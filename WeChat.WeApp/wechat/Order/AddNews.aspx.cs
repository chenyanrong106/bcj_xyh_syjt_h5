using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class AddNews :WeiPage
    {
        NewVerService nvbo = new NewVerService();
        public Pet_JiuZhu_Info jz = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
            {
                string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                if (Request.QueryString["id"] != null)
                {
                    jz = nvbo.GetJiuZhuInfo(int.Parse(Request.QueryString["id"]));
                    if ((jz != null && !string.IsNullOrEmpty(jz.FromUserName) && jz.FromUserName == user) || user == "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" || user == "oloJGvzwelDQjIKMMWC8Z6ngM7gk" || user == "oloJGvx3hLkCNadLGIpAMimz2Xwc")
                    {

                    }
                    else
                    {
                        Response.Redirect("chou.aspx");
                    }
                }

            }
            else
            {
                //Response.Redirect("");
            }
        }
    }
}