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
    public partial class OrderTime : WeiPage
    {
        IStoreService StoreService = new StoreService();
        public ORG_STORE_EX store = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    store = StoreService.QueryStoreById(int.Parse(Request.QueryString["id"]));
                }
                else if (Request.QueryString["para"] != null)
                {
                    string day = Request.QueryString["date"];
                    int storeid = int.Parse(Request.QueryString["s"]);
                    int jssex = Request.QueryString["jssex"] == null ? -1 : int.Parse(Request.QueryString["jssex"]);//技师类别
                    List<EMPLOYEE_SCHEDULE> elist = StoreService.GetScheduleList(storeid, day,jssex);
                    List<CUST_BOOKING> dlist = StoreService.GetBookDateList(storeid, day);
                    DateTime dt = DateTime.Parse(day + " 09:00");
                    //List<pbinfo> plist = new List<pbinfo>();
                    string html = "";
                    while (dt < DateTime.Parse(day + " 21:00"))
                    {
                        int state = 0;//0 未排班 1可预约 2约满
                        List<EMPLOYEE_SCHEDULE> pb = elist.Where(p => p.BEGIN_TIME <= dt && p.END_TIME >= dt.AddMinutes(59)).ToList(); //找到59分钟内的排班信息
                        foreach (var p in pb)
                        {
                            List<CUST_BOOKING> book = dlist.Where(d => d.STAFF_ID == p.EMPLOYEE_ID && d.BEGIN_DATE.AddMinutes(-30) <= dt && d.BEGIN_DATE.AddMinutes(30) >= dt).ToList();   //一个技师在指定时间前半小时即后半小时之后有无预约
                            if (book.Count == 0)        //有一个技师在指定时间无预约，则这个时间可以预约
                            {
                                state = 1;
                                break;
                            }
                        }
                        if (pb.Count > 0 && state == 0)
                            state = 2;//有排班，并且没有可预约时间，则显示约满
                        //plist.Add(new pbinfo { state = state, time = dt.ToString("HH:mm") });
                        if (state == 0)
                            html += "<span><em class='curhui'>" + dt.ToString("HH:mm") + "(休息)</em></span>";
                        else if (state == 1)
                        {
                            if (DateTime.Now > dt)
                                html += "<span><em class='curhui'>" + dt.ToString("HH:mm") + "(过时)</em></span>";
                            else
                                html += "<span><em onclick='yuyue(this)'>" + dt.ToString("HH:mm") + "</em></span>";
                        }
                        else
                            html += "<span><em class='curhui'>" + dt.ToString("HH:mm") + "(约满)</em></span>";
                        dt = dt.AddMinutes(30);
                    }
                    Response.Write(html);
                    Response.End();
                }
            }
        }
    }
}