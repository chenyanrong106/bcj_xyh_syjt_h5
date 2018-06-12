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
    public partial class sueccss : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        MySmallShopService mss = new MySmallShopService();
        public Pet_XXL_Order order = null;
        public OAauth_Log o = null;
        public Pet_JiuZhu_Info jz = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["oid"] != null)
            {
                string[] oids = Request.QueryString["oid"].Split(',');
                if (oids.Length > 2)
                {
                    order = nvbo.GetOrder(int.Parse(oids[0]));
                    if (order != null)
                    {
                        if (order.OrderNo == oids[2])
                        {
                            if (order.CreateTime.Value.AddMinutes(10) > DateTime.Now)//十分钟之内
                            {
                                o = mss.GetOA(order.FromUserName);
                                jz = nvbo.GetJiuZhuInfo(order.Source.Value);
                            }
                            else
                            {
                                Response.Redirect("chou.aspx?p="+oids[1]);
                                //Response.Write("<h1>本页面已过期，感谢您的支持！</h1>");
                                //Response.End();
                            }
                        }
                        else
                        {
                            Response.Write("<h1>感谢您的支持！</h1>");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("<h1>感谢您的支持！</h1>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("<h1>感谢您的支持！</h1>");
                    Response.End();
                }
            }
            else if (Request.QueryString["para"] != null)
            {
                order = nvbo.GetOrder(int.Parse(Request.Params["oid"]));
                if (order != null)
                {
                    if (order.OrderNo == Request.Params["orderno"])
                    {
                        if (order.CreateTime.Value.AddMinutes(30) > DateTime.Now)//十分钟之内
                        {
                            order.CourierRemark = Request.Params["remark"];
                            nvbo.SaveOrder(order);
                            Response.Write("{\"st\":0,\"msg\":\"提交成功\"}");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("{\"st\":1,\"msg\":\"本页面已过期\"}");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("{\"st\":1,\"msg\":\"提交成功\"}");
                        Response.End();
                    }
                }
            }
            else
            {
                Response.Write("<h1>感谢您的支持！</h1>");
                Response.End();
            }
        }
    }
}