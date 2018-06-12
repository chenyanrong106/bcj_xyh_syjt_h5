using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using SPACRM.Entity.Entities;
using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SPACRM.WebApp.wechat.SPA
{
    public partial class OrderOK : WeiPage
    {
        IStoreService StoreService = new StoreService();
        IServiceService ss = new ServiceService();
        public ORG_STORE_EX store = null;
        public PROD_SERVICE ps = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["storeid"] != null)
                {
                    store = StoreService.QueryStoreById(int.Parse(Request.QueryString["storeid"]));
                }
                if (Request.QueryString["sid"] != null)
                {
                    ps = ss.GetService(int.Parse(Request.QueryString["sid"]));
                }
            }
        }
    }
}