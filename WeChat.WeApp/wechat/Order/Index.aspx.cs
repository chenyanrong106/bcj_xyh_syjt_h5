using SPACRM.Business.ServiceImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.Order
{
    public partial class Index : WeiPage
    {
        NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {

            BaseLoad();

            if (!IsPostBack)
            {
                Response.Write(" <input type='hidden' value='" + Server.UrlEncode(AbsoluteUri) + "' id='url' />");
            }
        }
    }
}