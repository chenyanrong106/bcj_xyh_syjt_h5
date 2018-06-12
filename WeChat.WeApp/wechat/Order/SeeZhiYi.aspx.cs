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
    public partial class SeeZhiYi : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        public Pet_JiuZhu_ZhiYi j = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["para"] == null)
            {

                try
                {
                    if (Request.QueryString["id"] != null || Request.QueryString["state"] != null)
                    {
                        j = nvbo.GetZhiZi(int.Parse(Request.QueryString["id"] == null ? Request.QueryString["state"] : Request.QueryString["id"]));
                        if (j != null)
                        {

                        }
                    }

                }
                catch (Exception)
                {
                }
            }
        }
    }
}