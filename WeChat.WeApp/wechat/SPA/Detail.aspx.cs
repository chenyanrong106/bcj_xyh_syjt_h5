using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Interface;

namespace SPACRM.WebApp.wechat.SPA
{
    public partial class Detail : WeiPage
    {
        IServiceService ss = new ServiceService();
        public PROD_SERVICE ps = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    ps = ss.GetService(int.Parse(Request.QueryString["id"]));
                }
            }
        }
    }
}