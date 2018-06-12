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
    public partial class Index : WeiPage
    {
        IServiceService ss = new ServiceService();
        public List<PROD_SERVICE> servicelist = new List<PROD_SERVICE>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                servicelist = ss.GetSerivceList("8B4FEB36-B242-4069-A2A4-03459100C998");
            }
        }
    }
}