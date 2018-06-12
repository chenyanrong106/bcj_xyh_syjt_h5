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
    public partial class Address8 : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BaseLoad();

                if (Request.Params["para"] == null)
                {
                    Response.Write(" <input type='hidden'' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
                    if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                    {
                        string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                        string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                        Pet_XXL_Address add = nvbo.GetPetXXLAddress(user);
                        if (add != null)
                        {
                            xm.Value = add.Name;
                            sj.Value = add.Phone;
                            //jd.Value = add.JieDao;
                            address.Value = add.Address;
                        }
                    }
                }
                sid.Value = Request.QueryString["id"];


            }

            if (Request.QueryString["para"] != null)
            {
                if ((Request.QueryString["FromUserName"] != null || Session["FromUserName"] != null) && (Request.QueryString["ToUserName"] != null || Session["ToUserName"] != null))
                {
                    string user = Request.QueryString["FromUserName"] == null ? Session["FromUserName"].ToString() : Request.QueryString["FromUserName"].ToString();
                    string user2 = Request.QueryString["ToUserName"] == null ? Session["ToUserName"].ToString() : Request.QueryString["ToUserName"].ToString();
                    Pet_XXL_Address add = null;// nvbo.GetPetXXLAddress(user);
                    add = new Pet_XXL_Address();
                    add.Address = Request.Params["address"];
                    add.CreateTime = DateTime.Now;
                    add.FromUserName = user;
                    add.Sheng = Request.Params["s"];
                    add.City = Request.Params["c"];
                    add.JieDao = Request.Params["x"];
                    add.Name = Request.Params["xm"];
                    add.Phone = Request.Params["sj"];
                    add.PostCode = "xxl";
                    nvbo.SavePetXXLAddress(add);
                    Response.Write("zhifu8.aspx?id=" + Request.Params["id"]);
                    Response.End();
                }
            }
        }
    }
}