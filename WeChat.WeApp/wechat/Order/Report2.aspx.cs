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
    public partial class Report2 : System.Web.UI.Page
    {
        //public List<OrderReport> list1 = new List<OrderReport>();
        //public List<OrderReport2> list2 = new List<OrderReport2>();
        //public List<OrderReport2> list3 = new List<OrderReport2>();
        //public List<OrderReport> list4 = new List<OrderReport>();
        //public List<OrderReport> list5 = new List<OrderReport>();
        //public List<OrderReport> list6 = new List<OrderReport>();
        public NewVerService nvbo = new NewVerService();
        protected void Page_Load(object sender, EventArgs e)
        {
            //list1 = nvbo.GetOrderReport();
            //list2 = nvbo.GetOrderReport2();
            //list3 = nvbo.GetOrderReport3();
            //list4 = nvbo.GetOrderReport4();
            //list5 = nvbo.GetOrderReport5(1);
            //list6 = nvbo.GetOrderReport5(2);
        }
    }
}