using SPACRM.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SPACRM.Business.ServiceImpl;
using SPACRM.Entity.Entities;
using SPACRM.Entity;
using Newtonsoft.Json;

namespace SPACRM.WebApp.wechat.SPA
{
    public partial class Order : WeiPage
    {
        IStoreService StoreService = new StoreService();
        public List<ORG_STORE_EX> list = new List<ORG_STORE_EX>();
        protected void Page_Load(object sender, EventArgs e)
        {
            list = this.StoreService.GetStoreListByOrgId("");

            //List<EMPLOYEE_SCHEDULE> elist = StoreService.GetScheduleList(3, "2017-03-13");
            //List<CUST_BOOKING> dlist = StoreService.GetBookDateList(3, "2017-03-13");
            //DateTime dt = DateTime.Parse("2017-03-13" + " 09:00");
            //List<pbinfo> plist = new List<pbinfo>();
            //while (dt<DateTime.Parse("2017-03-13"+" 21:00"))
            //{
            //    int state = 0;//0 未排班 1可预约 2约满
            //    List<EMPLOYEE_SCHEDULE> pb = elist.Where(p => p.BEGIN_TIME <= dt && p.END_TIME >= dt.AddMinutes(59)).ToList(); //找到59分钟内的排班信息
            //    foreach (var p in pb)
            //    {
            //        List<CUST_BOOKING> book = dlist.Where(d=>d.STAFF_ID==p.EMPLOYEE_ID&&d.BEGIN_DATE.AddMinutes(-30)<=dt&&d.BEGIN_DATE.AddMinutes(30)>=dt).ToList();   //一个技师在指定时间前半小时即后半小时之后有无预约
            //        if (book.Count == 0)        //有一个技师在指定时间无预约，则这个时间可以预约
            //        {
            //            state = 1;
            //            break;
            //        }
            //    }
            //    if (pb.Count > 0 && state == 0)
            //        state = 2;//有排班，并且没有可预约时间，则显示约满
            //    plist.Add(new pbinfo { state = state, time = dt.ToString("HH:mm") });
            //    dt = dt.AddMinutes(30);
            //}
            //Response.Write(JsonConvert.SerializeObject(plist));

           
        }

        class pbinfo
        {
            public int state { get; set; }
            public string time { get; set; }
        }
    }
}