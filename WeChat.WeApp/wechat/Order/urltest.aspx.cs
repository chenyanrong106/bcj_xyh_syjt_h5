using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class urltest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
            {
                Response.Write(Session["FromUserName"]);
                Response.Write("<br>");
               
            }
            if (Request.QueryString["id"] != null && Request.QueryString["c"] != null)
            {
                Response.Write(Request.QueryString["id"]);
                Response.Write("<br>");
                Response.Write(Request.QueryString["c"]);
                Response.Write("<br>");
            }
        }
    }
}