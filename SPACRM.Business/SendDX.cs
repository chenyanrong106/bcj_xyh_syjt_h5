using SPACRM.Business.ServiceImpl;
using SPACRM.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class SendDX
{
    LoginService _ls = new LoginService();
    CommonService _cs = new CommonService();
    public SendDX()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 发送短信
    /// </summary>
    /// <param name="Message">信息内容</param>
    /// <param name="Phone">手机号码</param>
    /// <param name="ToUserName">商户微信号或ID</param>
    /// <returns>发送结果</returns>
    public string Send(SMS_MESSAGE sms)
    {
        string msg = "未发送短信";
        try
        {
            //ORG_INFO org = _ls.GetMerchants(sms.ORG_ID.Value);
            //if (org != null && org.SMS_PASSWORD != null && org.SMS_SIGNATURE != null && org.SMS_USER_NAME != null)
            //{
            //    string content = sms.CONTENT + "【" + org.SMS_SIGNATURE + "】";
            //    string url = "http://www.ztsms.cn:8800/sendXSms.do?username=" + org.SMS_USER_NAME + "&password=" + org.SMS_PASSWORD + "&mobile=" + sms.PHONE_NUMBER + "&content=" + content + "&dstime=&productid=676766&xh=";
            //    msg = PostRequest(url);
            //    sms.RESULT = msg;

            //    if (msg.Contains("1,"))
            //    {
            //        msg = "发送成功";
            //        sms.STATUS = 1;
            //    }
            //    else
            //    {
            //        msg = "发送失败";
            //        sms.STATUS = -1;
            //    }
            //    _cs.SaveSMS_MESSAGE(sms);
            //    url = "http://www.ztsms.cn:8800/balance.do?username=" + org.SMS_USER_NAME + "&password=" + org.SMS_PASSWORD + "&productid=677678";
            //    org.SMS_QTY = int.Parse(PostRequest(url));
            //    _ls.UpdateMerchants(org);
            //    if (org.SMS_QTY < 500)
            //    {
            //        SMS_MESSAGE s = _cs.GetSMS_MESSAGEBYORG(sms.ORG_ID.Value);
            //        if (s == null || s.CREATE_TIME.Value.AddDays(1) < DateTime.Now) //没提醒过或提醒超过一天
            //        {
            //            SendMessage(org.SMS_NOTIFY_PHONE, "短信通道剩余短信条数为" + org.SMS_QTY + "条，请及时处理.", org, sms);
            //        }
            //    }
            //}
        }
        catch (Exception)
        {
            msg = "发送出错";
        }
        return msg;
    }

    private void SendMessage(string phone, string message, ORG_INFO org, SMS_MESSAGE sms)
    {
        //if (phone != null && phone != "")
        //{
        //    string content = sms.CONTENT + "【" + org.SMS_SIGNATURE + "】";
        //    string url = "http://www.ztsms.cn:8800/sendXSms.do?username=" + org.SMS_USER_NAME + "&password=" + org.SMS_PASSWORD + "&mobile=" + phone + "&content=" + message + "&dstime=&productid=676766&xh=";
        //    string msg = PostRequest(url);
        //    sms.PHONE_NUMBER = phone;
        //    sms.CONTENT = message;
        //    sms.TYPE = 9;//提醒商户
        //    sms.CREATE_TIME = DateTime.Now;
        //    if (msg.Contains("1,"))
        //    {
        //        sms.STATUS = 1;
        //    }
        //    else
        //    {
        //        sms.STATUS = -1;
        //    }
        //    _cs.SaveSMS_MESSAGE(sms);
        //}
    }

    public string PostRequest(string url)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);  //定义请求对象，并设置好请求URL地址
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();    //定义响应对象，request在调用GetResponse方法事执行请求了，而不是在HttpWebRequest.Create的时候执行。
        Stream stream = response.GetResponseStream(); //定义一个流对象，来获取响应流
        StreamReader sr = new StreamReader(stream);  //定义一个流读取对象，读取响应流
        string responseHTML = sr.ReadToEnd();
        return responseHTML;
    }
}
