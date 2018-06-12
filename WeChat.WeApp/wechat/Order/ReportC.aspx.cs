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
    public partial class ReportC : System.Web.UI.Page
    {
        public NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["para"] == null)
                {
                    if (((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null)) || Session["sfyz"] != null)
                    {
                        string user = "";
                        string user2 = "";
                        if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                        {
                            user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                            user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        }
                        if (user == "oloJGv_apxGreADUhBSL4r7NnpVc" || user == "oloJGvzwelDQjIKMMWC8Z6ngM7gk" || user == "oloJGvyniJ-SLjTPKtIxtMRX-Oiw" || user == "oloJGvx3hLkCNadLGIpAMimz2Xwc" || user == "oloJGv4lWL-TS8yn8uo4Fu1rbVTI" || Session["sfyz"] != null)
                        {
                        }
                        else
                        {
                            Response.Redirect("../order/chou.aspx");
                        }
                    }
                }
                else if (Request.QueryString["para"] == "oa")
                {
                    if (Session["sfyz"] != null)
                    {
                        Response.Write("ok");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("error");
                        Response.End();
                    }
                }
                else if (Request.QueryString["para"] == "yz")
                {
                    if (Request.Params["mm"] != null && Request.Params["mm"] == "王金华")
                    {
                        Session["sfyz"] = "王金华";
                        Response.Write("ok");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("error");
                        Response.End();
                    }
                }

            }
            //list5 = nvbo.GetOrderReport5(1);
            //list6 = nvbo.GetOrderReport5(2);
        }
    }
}