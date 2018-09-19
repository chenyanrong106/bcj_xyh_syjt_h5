using Newtonsoft.Json;
using SPACRM.Business.XYH_Coupon;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SPACRM.WebApp
{
    public class WXCardCommon
    {

        public static string GetCardApi(string token)
        {
            XHYCouponService _xyhService = new XHYCouponService();
            string Cardapi_ticket = "";
            CardApiTicket at = new CardApiTicket();
            at = _xyhService.GetModelCardApi();
            if (at == null)
                at = new CardApiTicket();
            if (at.Cardapi_Ticket != "" && at.Cardapi_Ticket != null && (at.GetTicketTime == null ? DateTime.Now.AddHours(-3) : at.GetTicketTime.Value).AddHours(2) > DateTime.Now) //不为空，并且获取时间没有超过2小时
            {
                return at.Cardapi_Ticket;
            }
            else
            {
                Cardapi_ticket = at.Cardapi_Ticket;
                string url = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=wx_card";
                string b = PostRequest(url);
                CardApi ticket = JsonConvert.DeserializeObject<CardApi>(b);
                if (ticket.errcode == 0)  //正确
                {
                    at.Cardapi_Ticket = ticket.ticket;
                    at.GetTicketTime = DateTime.Now;
                    _xyhService.AddCardApi(at);
                    return at.Cardapi_Ticket;
                }
                return "";
            }
        }

        public static string PostRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
            request.Method = "post";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。       
            Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
            StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
            string responseHTML = sr.ReadToEnd();
            return responseHTML;
        }
    }
}