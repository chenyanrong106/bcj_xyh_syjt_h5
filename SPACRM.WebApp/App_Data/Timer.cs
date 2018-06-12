using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Configuration;
using Newtonsoft.Json;
using SPACRM.Entity;
using SPACRM.Business.ServiceImpl;
using SPACRM.Interface;
using SPACRM.Common;
using SPACRM.Entity.Entities;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using SPACRM.Common.Utils;
using FluentScheduler;
using System.Threading.Tasks;

/// <summary>
///WeiPage 的摘要说明
/// </summary>
public class Timer : WeiPage
{
    MySmallShopService mss = new MySmallShopService();
    public Timer()
    {
        Registry registry = new Registry();
        registry.Schedule(() =>
        Test()).WithName("Test").ToRunNow().AndEvery(6).Minutes();
        //JobManager.Initialize(registry);

        registry.Schedule(() =>
       Test2()).WithName("Test2").ToRunNow().AndEvery(1).Hours();
        JobManager.Initialize(registry);
    }

    public void Test()
    {
        try
        {

            List<WXMessageLog_EX> mlist = mss.GetOrderNoPayed();
            foreach (var m in mlist)
            {
                if (!string.IsNullOrEmpty(m.FromUserName))
                {
                    mss.SaveWXMessageLog(new ORG_WXMessage_Log { CreateTime = DateTime.Now, EventID = m.ID, EventType = 1 });
                    string token = Token(mjuserid);
                    var temp = new
                    {
                        first = new { value = "未支付提醒通知", color = "#173177" },
                        type = new { value = "项目", color = "#173177" },
                        e_title = new { value = "小时光SPA预约", color = "#173177" },
                        o_id = new { value = m.ORDER_NO, color = "#173177" },
                        order_date = new { value = m.CREATE_DATE.ToString("yyyy.MM.dd HH:mm"), color = "#173177" },
                        o_money = new { value = m.PAY_AMT, color = "#173177" },
                        remark = new { value = "\n" + "Hi，您预约的小时光SPA还未支付，超过10分钟订单自动取消哦~", color = "#CD0000" }
                    };
                    string message = SendTemplateMessage(token, m.FromUserName, "Or5LLJHk2GFu_9uLkeEKmCgvFU8p1BlokArSeAe6jaQ", "#FF0000", temp, WebUrl + "/wechat/spa/dingdan.aspx");
                }
            }

            List<WXMessageLog_EX> llist = mss.GetOrderClosed();
            foreach (var m in llist)
            {
                if (!string.IsNullOrEmpty(m.FromUserName))
                {
                    mss.SaveWXMessageLog(new ORG_WXMessage_Log { CreateTime = DateTime.Now, EventID = m.ID, EventType = 3 });
                    string token = Token(mjuserid);
                    var temp = new
                    {
                        first = new { value = "您好，您有一笔交易已关闭。", color = "#173177" },
                        keyword1 = new { value = m.ORDER_NO, color = "#173177" },
                        keyword2 = new { value = m.CREATE_DATE.ToString("yyyy.MM.dd HH:mm"), color = "#173177" },
                        keyword3 = new { value = "订单支付超时", color = "#173177" },
                        keyword4 = new { value = m.OverdueDate.ToString("yyyy.MM.dd HH:mm"), color = "#173177" },
                        remark = new { value = "\n" + "感谢您的选择，请及时查看确认。如有疑问，请咨询客服：176-2116-0808", color = "#CD0000" }
                    };
                    string message = SendTemplateMessage(token, m.FromUserName, "vxtsFe76nhOMAEXz_8GvgxsQbpAxBsR5iD0_uRXBuww", "#FF0000", temp, WebUrl + "/wechat/spa/dingdan.aspx");
                }
            }

        }
        catch (Exception ex)
        {
            mss.SaveLog(new WXLOG { CON = ex.Message + "," + ex.StackTrace, TIME = DateTime.Now });
        }
    }

    public void Test2()
    {
        try
        {
            List<WXMessageLog_EX> melist = mss.GetOrderTwoHour();
            foreach (var l in melist)
            {
                if (!string.IsNullOrEmpty(l.FromUserName))
                {
                    mss.SaveWXMessageLog(new ORG_WXMessage_Log { CreateTime = DateTime.Now, EventID = l.ID, EventType = 2 });
                    string token = Token(mjuserid);
                    var temp = new
                    {
                        first = new { value = "亲爱哒，距离您的小时光之旅还有2小时哦，我们再等着您的大驾光临啦~", color = "#173177" },
                        keyword1 = new { value = l.NAME, color = "#173177" },
                        keyword2 = new { value = "您的服务体验时间是" + l.BEGIN_DATE.ToString("yyyy.MM.dd HH:mm"), color = "#173177" },
                        remark = new { value = "\n" + "若计划有变，请找小星星修改预约时间哦~", color = "#CD0000" }
                    };
                    string message = SendTemplateMessage(token, l.FromUserName, "FEBQLhjUm51Qvk8unkCrjKPMJ8Nw8DL8z6PDh8smtGU", "#FF0000", temp, WebUrl + "/wechat/spa/dingdan.aspx");
                }
            }

        }
        catch (Exception ex)
        {
            mss.SaveLog(new WXLOG { CON = ex.Message + "," + ex.StackTrace, TIME = DateTime.Now });
        }
    }

}